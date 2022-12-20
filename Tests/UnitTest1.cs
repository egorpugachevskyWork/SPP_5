using MainPart;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace DITests;

public class Tests
{
    [Test]
    public void DefaultRegistrationTest()
    {
        var dependencies = new DependenciesConfiguration();
        dependencies.Register<IService1, Service1>();
        dependencies.Register<AbstractService2, Service2>();

        var provider = new DependencyProvider(dependencies);
        var service = provider.Resolve<IService1>();
        Assert.That(service.GetType().ToString(), Is.EqualTo("DITests.Service1"));
    }

    [Test]
    public void AbstractRegistrationTest()
    {
        var dependencies = new DependenciesConfiguration();
        dependencies.Register<AbstractService2, Service2>();

        var provider = new DependencyProvider(dependencies);
        var service = provider.Resolve<AbstractService2>();
        Assert.That(service.GetType().ToString(), Is.EqualTo("DITests.Service2"));
    }


    [Test]
    public void RegistrationAsSelfTest()
    {
        var dependencies = new DependenciesConfiguration();
        dependencies.Register<Service1, Service1>();

        var provider = new DependencyProvider(dependencies);
        var service = provider.Resolve<Service1>();
        Assert.That(service.GetType().ToString(), Is.EqualTo("DITests.Service1"));
    }

    [Test]
    public void RecursiveRegistrationTest()
    {
        var dependencies = new DependenciesConfiguration();
        dependencies.Register<IService, ServiceImpl>();
        dependencies.Register<IRepository, RepositoryImpl>();

        var provider = new DependencyProvider(dependencies);
        var service = provider.Resolve<IService>();
        Assert.That(service.GetType().ToString(), Is.EqualTo("DITests.ServiceImpl"));
    }

    [Test]
    public void SingletonRegistrationTest()
    {
        var dependencies = new DependenciesConfiguration();
        dependencies.Register<IService1, Service1>(LivingTime.Singleton);

        var provider = new DependencyProvider(dependencies);
        var service = provider.Resolve<IService1>();
        service.SomeData = "singleton";
        var newService = provider.Resolve<IService1>();
        Assert.That(newService.SomeData, Is.EqualTo("singleton"));
    }

    [Test]
    public void ManyImplementationTest()
    {
        var dependencies = new DependenciesConfiguration();
        dependencies.Register<IService1, Service1>();
        dependencies.Register<IService1, Service3>();
        
        var provider = new DependencyProvider(dependencies);
        IEnumerable<IService1> services = provider.Resolve<IEnumerable<IService1>>();
        Assert.That(services.Count(), Is.EqualTo(2));
    }

    [Test]
    public void GenericRegistrationTest()
    {
        var dependencies = new DependenciesConfiguration();
        dependencies.Register<IRepository, RepositoryImpl>();
        dependencies.Register<IService5<IRepository>, ServiceImpl5<IRepository>>();
        
        var provider = new DependencyProvider(dependencies);
        var service = provider.Resolve<IService5<IRepository>>();
        Assert.That(service.GetType().ToString(), Is.EqualTo("DITests.ServiceImpl5`1[DITests.IRepository]"));
    }

    [Test]
    public void OpenGenericTest()
    {
        var dependencies = new DependenciesConfiguration();
        dependencies.Register(typeof(IService5<>), typeof(ServiceImpl5<>));
        dependencies.Register<IRepository, RepositoryImpl>();
        
        var provider = new DependencyProvider(dependencies);
        var service = provider.Resolve<IService5<IRepository>>();
        Assert.That(service.GetType().ToString(), Is.EqualTo("DITests.ServiceImpl5`1[DITests.IRepository]"));
    }

    enum ServiceImplementations
    {
        First,
        Second
    }

    [Test]
    public void EnumTest()
    {
        var dependencies = new DependenciesConfiguration();
        dependencies.Register<IService1, Service1>(LivingTime.InstancePerDependency, ServiceImplementations.First);
        dependencies.Register<IService1, Service3>(LivingTime.InstancePerDependency, ServiceImplementations.Second);

        var provider = new DependencyProvider(dependencies);
        var first = provider.Resolve<IService1>(ServiceImplementations.First);
        var second = provider.Resolve<IService1>(ServiceImplementations.Second);
        Assert.That(first.GetType().ToString(), Is.EqualTo("DITests.Service1"));
        Assert.That(second.GetType().ToString(), Is.EqualTo("DITests.Service3"));
    }
}