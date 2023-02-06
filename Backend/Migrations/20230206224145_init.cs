using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Certificates_CertificateId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CertificateId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CertificateId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "MediaTemplatePath",
                table: "Certificates");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Certificates",
                newName: "CertificateTemplateId");

            migrationBuilder.AddColumn<long>(
                name: "CertificateTemplateId",
                table: "Courses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "CertificateTemplates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MediaPath = table.Column<string>(type: "text", nullable: false),
                    TextContent = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificateTemplates", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CertificateTemplateId",
                table: "Courses",
                column: "CertificateTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_CertificateTemplateId",
                table: "Certificates",
                column: "CertificateTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificates_CertificateTemplates_CertificateTemplateId",
                table: "Certificates",
                column: "CertificateTemplateId",
                principalTable: "CertificateTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CertificateTemplates_CertificateTemplateId",
                table: "Courses",
                column: "CertificateTemplateId",
                principalTable: "CertificateTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificates_CertificateTemplates_CertificateTemplateId",
                table: "Certificates");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CertificateTemplates_CertificateTemplateId",
                table: "Courses");

            migrationBuilder.DropTable(
                name: "CertificateTemplates");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CertificateTemplateId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Certificates_CertificateTemplateId",
                table: "Certificates");

            migrationBuilder.DropColumn(
                name: "CertificateTemplateId",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "CertificateTemplateId",
                table: "Certificates",
                newName: "CourseId");

            migrationBuilder.AddColumn<long>(
                name: "CertificateId",
                table: "Courses",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MediaTemplatePath",
                table: "Certificates",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CertificateId",
                table: "Courses",
                column: "CertificateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Certificates_CertificateId",
                table: "Courses",
                column: "CertificateId",
                principalTable: "Certificates",
                principalColumn: "Id");
        }
    }
}
