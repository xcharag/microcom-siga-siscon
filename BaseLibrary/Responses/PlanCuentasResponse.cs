namespace BaseLibrary.Responses;

public record PlanCuentasResponse(bool Flag, int CodCuenta, int Nivel, string Message = null!);
