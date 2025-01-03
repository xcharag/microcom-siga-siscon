using BaseLibrary.Entities;

namespace ServerLibrary.Helpers;

public class CorrelativoHelpers
{
    public static string TakeOutPrefijo(string correlativo, string prefijo)
    {
        if (string.IsNullOrEmpty(prefijo)) return correlativo;
        var correlativoSinPrefijo = correlativo.Substring(prefijo.Length);
        return correlativoSinPrefijo;
    }
    
    public static string TakeOutSufijo(string correlativo, string sufijo)
    {
        if (string.IsNullOrEmpty(sufijo)) return correlativo;
        var correlativoSinSufijo = correlativo.Substring(0,correlativo.Length - sufijo.Length);
        return correlativoSinSufijo;
    }
    
    public static string TakeOutGestion(string correlativo, string gestion)
    {
        if (string.IsNullOrEmpty(gestion)) return correlativo;
        var correlativoSinGestion = correlativo.Replace(gestion, "");
        return correlativoSinGestion;
    }
    
    public static string TakeOutRelleno(string correlativo, string relleno)
    {
        if (string.IsNullOrEmpty(relleno)) return correlativo;
        var correlativoSinRelleno = correlativo.Replace(relleno, "");
        return correlativoSinRelleno;
    }
    
    public static string VerifyCorrelativo(string correlativo, Correlativo correlativoEntity)
    {
        var searchValues = new List<string>();

        if (correlativoEntity.Prefijo != null) searchValues.Add(correlativoEntity.Prefijo);
        if (correlativoEntity.Sufijo != null) searchValues.Add(correlativoEntity.Sufijo);
        if (correlativoEntity.Gestion != null) searchValues.Add(correlativoEntity.Gestion);
        
        var containsAll = searchValues.All(correlativo.Contains);
        
        correlativo = TakeOutPrefijo(correlativo, correlativoEntity.Prefijo!);
        correlativo = TakeOutSufijo(correlativo, correlativoEntity.Sufijo!);
        correlativo = TakeOutGestion(correlativo, correlativoEntity.Gestion!);
        var correlativoSinRelleno = TakeOutRelleno(correlativo, correlativoEntity.Relleno!);
        
        if (containsAll && correlativoSinRelleno.Length > 0)
        {
            return "Ok";
        }
        return "Error";
    }

    public static string BuildCorrelativo(Correlativo correlativo)
    {
        var correlativoCompleto = string.Empty;

        if (correlativo.Prefijo != null) correlativoCompleto += correlativo.Prefijo;
        if (correlativo.Gestion != null) correlativoCompleto += correlativo.Gestion;
        if (correlativo.Valor != null) correlativoCompleto += correlativo.Valor;
        if (correlativo.Sufijo != null) correlativoCompleto += correlativo.Sufijo;

        return correlativoCompleto;
    }
    
    public static string ExtractValor(string correlativo, Correlativo correlativoEntity)
    {
        var correlativoSinPrefijo = TakeOutPrefijo(correlativo, correlativoEntity.Prefijo!);
        var correlativoSinSufijo = TakeOutSufijo(correlativoSinPrefijo, correlativoEntity.Sufijo!);
        var correlativoSinGestion = TakeOutGestion(correlativoSinSufijo, correlativoEntity.Gestion!);
        var valorCorrelativo = TakeOutRelleno(correlativoSinGestion, correlativoEntity.Relleno!);
        return valorCorrelativo;
    }
}