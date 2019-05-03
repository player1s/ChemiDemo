namespace ChemiDemo.DataContext.Schema
{
    using FluentMigrator;

    [Migration(201711281439)]
    public class CreateDatabase
        : Migration
    {
        public override void Down()
        {
            Delete.Table("tblProduct");
        }

        public override void Up()
        {
            Create.Table("tblProduct")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Uri").AsString(200).Nullable()
                .WithColumn("ProductName").AsString(250).Nullable()
                .WithColumn("SupplierName").AsString(250).Nullable()
                .WithColumn("IsUpdated").AsBoolean().WithDefaultValue(false).NotNullable()
                .WithColumn("UserName").AsString(250).Nullable()
                .WithColumn("Password").AsString(250).Nullable()
                .WithColumn("Content").AsBinary().Nullable()
                .WithColumn("ContentType").AsString(100).Nullable()
                .WithColumn("StatusCode").AsInt32().Nullable()
                .WithColumn("LastUpdate").AsDateTime().Nullable();
        }
    }
}