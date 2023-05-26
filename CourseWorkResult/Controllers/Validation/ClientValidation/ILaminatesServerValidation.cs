namespace CourseWorkResult.Controllers.Validation.ClientValidation
{
    interface ILaminatesServerValidation
    {
        bool CheckIpAddress(string ipAddress);
        bool CheckPort(int port);
    }
}
