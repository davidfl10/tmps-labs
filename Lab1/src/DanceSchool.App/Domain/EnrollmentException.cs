namespace DanceSchool.App.Domain;

/// <summary>Raised when an enrollment or check-in breaks a business rule.</summary>
public class EnrollmentException(string message) : Exception(message);
