using System;

public static class ActionInvoker
{
    public static void Invoke(Action action)
    {
        if (action != null)
            action();
    }

    public static void Invoke<T>(Action<T> action, T arg1)
    {
        if (action != null)
            action(arg1);
    }
}