namespace NetworkConsistency.Domain.Errors
{
    public enum DomainErrorTypes
    {
        REPORT_IS_IN_WORK,
        FINISHDATE_IS_SET,
        
        NO_FAILED_SENSORS,
        SECTION_ALREADY_EXISTS,
        CANNOT_BIND_EMPTY_SECTION,
        
        SENSOR_NOT_FOUND,
        SECTION_NOT_FOUND
    }
}