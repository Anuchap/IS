using Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Data.Mapping
{
    public class EmployeeMap : EntityTypeConfiguration<Employee>
    {
        public EmployeeMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Table & Column Mappings
            ToTable("dbo.Employees");
            Property(t => t.Id).HasColumnName("EmployeeID");
            Property(t => t.FirstNameEn).HasColumnName("FirstName_En");
            Property(t => t.LastNameEn).HasColumnName("LastName_En");
        }
    }
}