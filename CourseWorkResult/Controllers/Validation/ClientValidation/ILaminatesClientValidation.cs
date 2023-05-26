namespace CourseWorkResult.Controllers.Validation.ClientValidation
{
    interface ILaminatesClientValidation
    {
        bool CheckIpAddress(string ipAddress);
        bool CheckPort(int port);
    }
}
