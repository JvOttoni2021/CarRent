namespace CarRent.API.Exceptions
{
    public class ValidationAppException : Exception
    {
        public IReadOnlyDictionary<string, string[]> Errors { get; set; }

        public ValidationAppException(IReadOnlyDictionary<string, string[]> errors) : base("Erro de validação encontrado") => Errors = errors;
    }
}
