using FluentResults;

namespace NetworkConsistency.Domain.Errors
{
    public class DomainError: Error
    {
        private  DomainError(DomainErrorTypes type) : base(GetErrorDescription(type)) { }
        private DomainError(DomainErrorTypes type, Error causedBy) : base(GetErrorDescription(type), causedBy) { }

        public static DomainError Create(DomainErrorTypes type) => new(type);
        public static DomainError Create(DomainErrorTypes type, Error causedBy) => new(type, causedBy);

        private static string GetErrorDescription(DomainErrorTypes type) => type switch
        {
            DomainErrorTypes.REPORT_IS_IN_WORK => "Отчет уже находится в работе",
            DomainErrorTypes.FINISHDATE_IS_SET => "Обработка отчета уже завершена",
            DomainErrorTypes.NO_FAILED_SENSORS => "Все датчики работают корректно",
            DomainErrorTypes.SECTION_ALREADY_EXISTS => "Секция уже привязана к датчику",
            DomainErrorTypes.CANNOT_BIND_EMPTY_SECTION => "Невозможно отвязать секцию от датчика",
            DomainErrorTypes.SENSOR_NOT_FOUND => "Датчик не найден",
            DomainErrorTypes.SECTION_NOT_FOUND => "Секция не найдена",
            _ => "Неизвестная ошибка"
        };
    }
}