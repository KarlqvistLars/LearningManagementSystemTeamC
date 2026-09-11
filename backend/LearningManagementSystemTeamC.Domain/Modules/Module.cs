using LearningManagementSystemTeamC.Domain.Courses;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace LearningManagementSystemTeamC.Domain.Modules;

public class Module
{
public Guid Id { get; private set; }
    public string ModuleName { get; private set; }
    public string Description { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public Guid CourseId { get; private set; }

    public Module(
        string moduleName,
        string description,
        DateTime startDate,
        DateTime endDate,
        Guid courseId)
    {
        Validate(
            moduleName,
            description,
            startDate,
            endDate,
            courseId);

        Id = Guid.NewGuid();
        ModuleName = moduleName;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        CourseId = courseId;
    }

    public void Update(string name, string description, DateTime startDate, DateTime endDate, Guid courseId)
    {
        Validate(
            name,
            description, 
            startDate, 
            endDate, 
            courseId);
            
        ModuleName = name;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
    }

    private static void Validate(
        string moduleName,
        string description,
        DateTime startDate,
        DateTime endDate,
        Guid courseId)
    {
        if (string.IsNullOrWhiteSpace(moduleName))
            throw new DomainException(
                ModuleRules.ModuleNameRequiredCode,
                ModuleRules.ModuleNameRequiredMessage);

        if (string.IsNullOrWhiteSpace(description))
            throw new DomainException(
                ModuleRules.ModuleDescriptionRequiredCode,
                ModuleRules.ModuleDescriptionRequiredMessage);

        if (endDate <= startDate)
            throw new DomainException(
                ModuleRules.InvalidModuleDateCode,
                ModuleRules.InvalidModuleDateMessage);
                
        if (courseId == Guid.Empty)
            throw new DomainException(
                ModuleRules.CourseIdRequiredCode,
                ModuleRules.CourseIdRequiredMessage);
    }

}