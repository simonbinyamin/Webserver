using System;

public static class EnvironmentExtensions
{
    public static bool IsProductionEnvironment(this string environment)
    {
        if(String.IsNullOrEmpty(environment)){
            return false;
        }
        return environment == "PROD";
    }
}