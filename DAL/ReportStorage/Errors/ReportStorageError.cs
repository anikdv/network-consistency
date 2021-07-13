using FluentResults;

namespace ReportStorage.Errors
{
    public class ReportStorageError: Error
    {
        public ReportStorageErrorTypes ErrorType { get; }

        private  ReportStorageError(ReportStorageErrorTypes type) : base(GetErrorDescription(type)) { }
        private ReportStorageError(ReportStorageErrorTypes type, Error causedBy) : base(GetErrorDescription(type), causedBy) { }

        public static ReportStorageError Create(ReportStorageErrorTypes type) => new(type);
        public static ReportStorageError Create(ReportStorageErrorTypes type, Error causedBy) => new(type, causedBy);

        private static string GetErrorDescription(ReportStorageErrorTypes type) => type switch
        {
            _ => "Неизвестная ошибка"
        };
    }
}