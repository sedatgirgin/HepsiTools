namespace HepsiTools.GenericRepositories.ResultMessage
{
    interface IErrorResult : IResult
    {
        object Errors { get; set; }
    }
}
