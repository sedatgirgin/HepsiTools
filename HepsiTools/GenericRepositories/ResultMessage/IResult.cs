namespace HepsiTools.GenericRepositories.ResultMessage
{
    interface IResult
    {
        string Message { get; set; }
        bool Succeeded { get; set; }
    }
}
