using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "varchar(180)", nullable: false),
                    Phone = table.Column<string>(type: "varchar(64)", nullable: true),
                    Password = table.Column<string>(type: "varchar(255)", nullable: false),
                    Image = table.Column<string>(type: "varchar(255)", nullable: true),
                    FirstName = table.Column<string>(type: "varchar(191)", nullable: true),
                    LastName = table.Column<string>(type: "varchar(191)", nullable: true),
                    Gender = table.Column<string>(type: "varchar(2)", nullable: true),
                    Country = table.Column<string>(type: "varchar(191)", nullable: true),
                    JobTitle = table.Column<string>(type: "varchar(255)", nullable: true),
                    Facebook = table.Column<string>(type: "varchar(255)", nullable: true),
                    Instagram = table.Column<string>(type: "varchar(255)", nullable: true),
                    Twitter = table.Column<string>(type: "varchar(255)", nullable: true),
                    LinkedIn = table.Column<string>(type: "varchar(255)", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    AboutMe = table.Column<string>(type: "text", nullable: true),
                    ResetToken = table.Column<string>(type: "varchar(36)", nullable: true),
                    ConfirmToken = table.Column<string>(type: "varchar(36)", nullable: true),
                    Confirmed = table.Column<short>(type: "smallint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Activity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Event = table.Column<string>(type: "varchar(255)", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activity_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Image = table.Column<string>(type: "varchar(255)", nullable: true),
                    Title = table.Column<string>(type: "varchar(255)", nullable: false),
                    Slug = table.Column<string>(type: "varchar(255)", nullable: false),
                    Description = table.Column<string>(type: "varchar(255)", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Categories = table.Column<string>(type: "text", nullable: true),
                    Tags = table.Column<string>(type: "text", nullable: true),
                    TotaViewer = table.Column<int>(type: "int", nullable: false),
                    TotaComment = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Article_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Subject = table.Column<string>(type: "varchar(255)", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    ArticleId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Body = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment_Comment_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Comment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Viewer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Viewer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Viewer_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Viewer_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activity_CreatedAt",
                table: "Activity",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Activity_Event",
                table: "Activity",
                column: "Event");

            migrationBuilder.CreateIndex(
                name: "IX_Activity_UpdatedAt",
                table: "Activity",
                column: "UpdatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Activity_UserId",
                table: "Activity",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Article_CreatedAt",
                table: "Article",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Article_Description",
                table: "Article",
                column: "Description");

            migrationBuilder.CreateIndex(
                name: "IX_Article_Image",
                table: "Article",
                column: "Image");

            migrationBuilder.CreateIndex(
                name: "IX_Article_Slug",
                table: "Article",
                column: "Slug");

            migrationBuilder.CreateIndex(
                name: "IX_Article_Status",
                table: "Article",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Article_Title",
                table: "Article",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_Article_TotaComment",
                table: "Article",
                column: "TotaComment");

            migrationBuilder.CreateIndex(
                name: "IX_Article_TotaViewer",
                table: "Article",
                column: "TotaViewer");

            migrationBuilder.CreateIndex(
                name: "IX_Article_UpdatedAt",
                table: "Article",
                column: "UpdatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Article_UserId",
                table: "Article",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ArticleId",
                table: "Comment",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_CreatedAt",
                table: "Comment",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ParentId",
                table: "Comment",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_UpdatedAt",
                table: "Comment",
                column: "UpdatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_UserId",
                table: "Comment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_CreatedAt",
                table: "Notification",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_Subject",
                table: "Notification",
                column: "Subject");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UpdatedAt",
                table: "Notification",
                column: "UpdatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UserId",
                table: "Notification",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Confirmed",
                table: "User",
                column: "Confirmed");

            migrationBuilder.CreateIndex(
                name: "IX_User_ConfirmToken",
                table: "User",
                column: "ConfirmToken");

            migrationBuilder.CreateIndex(
                name: "IX_User_Country",
                table: "User",
                column: "Country");

            migrationBuilder.CreateIndex(
                name: "IX_User_CreatedAt",
                table: "User",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_User_Facebook",
                table: "User",
                column: "Facebook");

            migrationBuilder.CreateIndex(
                name: "IX_User_FirstName",
                table: "User",
                column: "FirstName");

            migrationBuilder.CreateIndex(
                name: "IX_User_Gender",
                table: "User",
                column: "Gender");

            migrationBuilder.CreateIndex(
                name: "IX_User_Image",
                table: "User",
                column: "Image");

            migrationBuilder.CreateIndex(
                name: "IX_User_Instagram",
                table: "User",
                column: "Instagram");

            migrationBuilder.CreateIndex(
                name: "IX_User_LastName",
                table: "User",
                column: "LastName");

            migrationBuilder.CreateIndex(
                name: "IX_User_LinkedIn",
                table: "User",
                column: "LinkedIn");

            migrationBuilder.CreateIndex(
                name: "IX_User_Password",
                table: "User",
                column: "Password");

            migrationBuilder.CreateIndex(
                name: "IX_User_Phone",
                table: "User",
                column: "Phone");

            migrationBuilder.CreateIndex(
                name: "IX_User_ResetToken",
                table: "User",
                column: "ResetToken");

            migrationBuilder.CreateIndex(
                name: "IX_User_Twitter",
                table: "User",
                column: "Twitter");

            migrationBuilder.CreateIndex(
                name: "IX_User_UpdatedAt",
                table: "User",
                column: "UpdatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Viewer_ArticleId",
                table: "Viewer",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Viewer_CreatedAt",
                table: "Viewer",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Viewer_Status",
                table: "Viewer",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Viewer_UpdatedAt",
                table: "Viewer",
                column: "UpdatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Viewer_UserId",
                table: "Viewer",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activity");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "Viewer");

            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
