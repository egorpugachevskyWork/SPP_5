using System;

namespace DITests;

interface IService
{
}

class ServiceImpl : IService
{
    public ServiceImpl(IRepository repository)
    {
    }

    public ServiceImpl(int f, int d)
    {
    }
}

interface IRepository
{
}

class RepositoryImpl : IRepository
{
    public RepositoryImpl()
    {
    }
}

public interface IService1
{
    public string SomeData { get; set; }
}

public class Service1 : IService1
{
    public string SomeData { get; set; }

    public void DoSomething()
    {
        Console.WriteLine("Do something");
    }
}

public class Service3 : IService1
{
    public string SomeData { get; set; }
}

public abstract class AbstractService2
{
    public void DoSomething()
    {
        Console.WriteLine("Do something");
    }
}

public class Service2 : AbstractService2
{
}

interface IService5<TRepository> where TRepository : IRepository
{
}

class ServiceImpl5<TRepository> : IService5<TRepository>
    where TRepository : IRepository
{
    public ServiceImpl5(TRepository repository)
    {
    }
}