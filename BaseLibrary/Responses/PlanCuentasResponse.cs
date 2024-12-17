namespace BaseLibrary.Responses;

public record PlanCuentasResponse(bool Flag, string CodCuenta, int Nivel, string Message = null!, bool IsFirstCuenta = false, bool IsDetalle = false);
