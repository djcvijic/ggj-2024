public abstract class Singleton<T> where T : Singleton<T>, new()
{
    private static T instance;

    public static T Instance => instance ??= new T();
}