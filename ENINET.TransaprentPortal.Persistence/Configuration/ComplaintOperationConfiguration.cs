using ENINET.TransparentPortal.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace ENINET.TransparentPortal.Persistence.Configuration;

public class ComplaintOperationConfiguration : IEntityTypeConfiguration<ComplaintOperation>
{
    public static string Opened = "OPENED";
    public static string Solved = "SOLVED";
    public static string Canceled = "CANCELED";
    public static string Action = "ACTION";

    public void Configure(EntityTypeBuilder<ComplaintOperation> builder)
    {
        builder.HasData(
            new ComplaintOperation { OperationId = Guid.NewGuid(), OperationName = Opened },
            new ComplaintOperation { OperationId = Guid.NewGuid(), OperationName = Solved },
            new ComplaintOperation { OperationId = Guid.NewGuid(), OperationName = Canceled },
            new ComplaintOperation { OperationId = Guid.NewGuid(), OperationName = Action }
       );
    }
}
