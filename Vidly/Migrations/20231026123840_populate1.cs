using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vidly.Migrations
{
    /// <inheritdoc />
    public partial class populate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE MembershipType SET Name = 'Pay as you go', SignUpFee = 0, DurationInMonths = 0, DiscountRate = 0 WHERE Id = 1");
            migrationBuilder.Sql("UPDATE MembershipType SET Name = 'Monthly', SignUpFee = 30, DurationInMonths = 1, DiscountRate = 10 WHERE Id = 2");
            migrationBuilder.Sql("UPDATE MembershipType SET Name = 'Quarterly', SignUpFee = 90, DurationInMonths = 3, DiscountRate = 15 WHERE Id = 3");
            migrationBuilder.Sql("UPDATE MembershipType SET Name = 'Yearly', SignUpFee = 300, DurationInMonths = 12, DiscountRate = 20 WHERE Id = 4");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
