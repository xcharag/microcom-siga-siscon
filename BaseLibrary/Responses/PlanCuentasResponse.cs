namespace BaseLibrary.Responses;

public record PlanCuentasResponse(bool Flag, string CodCuenta, int Nivel, string Message = null!);
