using BaseLibrary.Entities;

namespace ServerLibrary.Helpers;

public static class PlanCuentaHelpers
{
    public static string GetCuentaPadre(string id, int cuantos)
    {
        var cuentaPadre = id.Substring(0, id.Length - cuantos - 1);
        return cuentaPadre;
    }
    
    public static string GetLastNivelDigits(string id, int cuantos)
    {
        var lastNivelDigits = id.Substring(id.Length - cuantos);
        return lastNivelDigits;
    }
    
    public static int GetLargoCuenta(string id)
    {
        var idWithoutDots = id.Replace(".", "");
        return idWithoutDots.Length;
    }
}