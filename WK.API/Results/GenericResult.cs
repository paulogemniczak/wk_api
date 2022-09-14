namespace WK.API.Results
{
    /// <summary>
    /// 
    /// </summary>
    public class GenericResult
    {
        /// <summary>
        ///     Array com todos os erros ocorridos na requisição.
        /// </summary>
        public string[]? Errors { get; set; }

        /// <summary>
        ///     Propriedade que indica se a requisição ocorreu com sucesso ou com erros.
        /// </summary>
        public bool Success { get; set; }
    }

    public class GenericResult<TResult> : GenericResult
    {
        /// <summary>
        ///     Resultado da requisição.
        /// </summary>
        public TResult? Result { get; set; }

        /// <summary>
        ///     Total de itens resultantes na requisição.
        ///     Só é utilizado em listas de endpoints que fazem paginação no lado do servidor.
        /// </summary>
        public int? TotalElements { get; set; }
    }
}
