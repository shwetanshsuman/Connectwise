using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConnectWiseBackend.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MasterEmail",
                columns: table => new
                {
                    MasterEmailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailSubject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailBody = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterEmail", x => x.MasterEmailId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Subscription",
                columns: table => new
                {
                    SubscriptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimePeriodInMonths = table.Column<int>(type: "int", nullable: false),
                    SubscriptionFees = table.Column<float>(type: "real", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscription", x => x.SubscriptionId);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubscriptionId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.CompanyId);
                    table.ForeignKey(
                        name: "FK_Company_Subscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscription",
                        principalColumn: "SubscriptionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Branch",
                columns: table => new
                {
                    BranchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LandMark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pincode = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.BranchId);
                    table.ForeignKey(
                        name: "FK_Branch_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    InvoiceTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<int>(type: "int", nullable: false),
                    DueAmount = table.Column<int>(type: "int", nullable: false),
                    GeneratedForEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeneratedById = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoice_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobRole = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reminder",
                columns: table => new
                {
                    ReminderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Frequency = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reminder", x => x.ReminderId);
                    table.ForeignKey(
                        name: "FK_Reminder_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PayerId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ModeofPayment = table.Column<int>(type: "int", nullable: false),
                    DateOfPayemnt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transaction_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReminderLog",
                columns: table => new
                {
                    ReminderLogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReminderId = table.Column<int>(type: "int", nullable: false),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReminderLog", x => x.ReminderLogId);
                    table.ForeignKey(
                        name: "FK_ReminderLog_Reminder_ReminderId",
                        column: x => x.ReminderId,
                        principalTable: "Reminder",
                        principalColumn: "ReminderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MasterEmail",
                columns: new[] { "MasterEmailId", "EmailBody", "EmailName", "EmailSubject" },
                values: new object[,]
                {
                    { 1, "<!DOCTYPE html>\r\n<html>\r\n  <head>\r\n    <title>Company Registration</title>\r\n    <style>\r\n      /* Define any styles you want to apply to your email here */\r\n      body {{\r\n        font-family: Arial, sans-serif;\r\n        font-size: 16px;\r\n        color: #333;\r\n        line-height: 1.5;\r\n      }}\r\n      h1 {{\r\n        font-size: 24px;\r\n        margin-bottom: 10px;\r\n      }}\r\n      p {{\r\n        margin-bottom: 20px;\r\n      }}\r\n      strong {{\r\n        color: #007bff;\r\n      }}\r\n      .footer {{\r\n        margin-top: 50px;\r\n        font-size: 14px;\r\n        color: #666;\r\n      }}\r\n    </style>\r\n  </head>\r\n  <body>\r\n    <h1>Hello, {0}'s Admin</h1>\r\n\r\n    <p>Thank you for signing up for our service. Your company has been successfully registered with ConnectWise.</p>\r\n\r\n    <p>Our Team at ConnectWise will always be available to assist you.</p>\r\n    <a href=\"{1}\" class=\"button\" style=\"background-color: #f44336; color: #fff; padding: 10px 20px; border: none; border-radius: 5px;\">ConnectWise</a>\r\n\r\n    <p>If you have any questions or concerns, please don't hesitate to contact us.</p>\r\n\r\n    <p class=\"footer\">Best regards,<br>ConnectWise Team</p>\r\n  </body>\r\n</html>\r\n", "Company Registration", "Company registered successfully" },
                    { 2, "<!DOCTYPE html>\r\n <html>\r\n  <head>\r\n    <title>User Registration</title>\r\n    <style>\r\n      /* Define any styles you want to apply to your email here */\r\n      body {{\r\n        font-family: Arial, sans-serif;\r\n        font-size: 16px;\r\n        color: #333;\r\n        line-height: 1.5;\r\n      }}\r\n      h1 {{\r\n        font-size: 24px;\r\n        margin-bottom: 10px;\r\n      }}\r\n      p {{\r\n        margin-bottom: 20px;\r\n      }}\r\n      strong {{\r\n        color: #007bff;\r\n      }}\r\n      .footer {{\r\n        margin-top: 50px;\r\n        font-size: 14px;\r\n        color: #666;\r\n      }}\r\n      table {{\r\n        border-collapse: collapse;\r\n        width: 100%;\r\n        border: 1px solid #000;\r\n      }}\r\n      th, td {{\r\n        padding: 8px;\r\n        text-align: left;\r\n        border-bottom: 1px solid #000;\r\n        border-right: 1px solid #000;\r\n      }}\r\n      th:last-child, td:last-child {{\r\n        border-right: none;\r\n      }}\r\n    </style>\r\n  </head>\r\n  <body>\r\n    <h1>Hello, sir/madam</h1>\r\n    <p>Congratulations! You have been successfully registered on our application by your company {0}. Your username is {1} and password is {2}. We kindly request you to change your password using the 'Forgot Password' option.</p>\r\n\r\n    <p>Our Team at ConnectWise will always be available to assist you</p>\r\n    <a href=\"{3}\" class=\"button\" style=\"background-color: #f44336; color: #fff; padding: 10px 20px; border: none; border-radius: 5px;\"\">ConnectWise</a>\r\n\r\n    <p>If you have any questions or concerns, please don't hesitate to contact us.</p>\r\n\r\n    <p class=\"footer\">Best regards,<br>ConnectWise Team</p>\r\n  </body>\r\n</html>", "User Registration", "User registered successfully" },
                    { 3, "<!DOCTYPE html>\r\n\r\n<html>\r\n  <head>\r\n    <title>Reminder</title>\r\n    <style>\r\n      /* Define any styles you want to apply to your email here */\r\n      body {{\r\n        font-family: Arial, sans-serif;\r\n        font-size: 16px;\r\n        color: #333;\r\n        line-height: 1.5;\r\n      }}\r\n      h1 {{\r\n        font-size: 24px;\r\n        margin-bottom: 10px;\r\n      }}\r\n      p {{\r\n        margin-bottom: 20px;\r\n      }}\r\n      strong {{\r\n        color: #007bff;\r\n      }}\r\n      .footer {{\r\n        margin-top: 50px;\r\n        font-size: 14px;\r\n        color: #666;\r\n      }}\r\n      table {{\r\n        border-collapse: collapse;\r\n        width: 100%;\r\n        border: 1px solid #000;\r\n      }}\r\n      th, td {{\r\n        padding: 8px;\r\n        text-align: left;\r\n        border-bottom: 1px solid #000;\r\n        border-right: 1px solid #000;\r\n      }}\r\n      th:last-child, td:last-child {{\r\n        border-right: none;\r\n      }}\r\n    </style>\r\n  </head>\r\n  <body>\r\n    <h1>Hello, sir/madam</h1>\r\n    <p>This email is being delivered to you by {0} for the invoice {1}</p>\r\n    <p>Below, you will find a Pay Button which will redirect you to the payment gateway, where you will be able to pay for the invoice:</p>\r\n    <table>\r\n      <tr>\r\n        <th>Invoice</th>\r\n        <th></th>\r\n      </tr>\r\n      <tr>\r\n        <td>Name</td>\r\n        <td>{2}</td>\r\n      </tr>\r\n      <tr>\r\n        <td>Total Amount</td>\r\n        <td>{3}</td>\r\n      </tr>\r\n      <tr>\r\n        <td>Due Amount</td>\r\n        <td>{4}</td>\r\n      </tr>\r\n    </table>\r\n    <a href=\"{5}\" class=\"button\" style=\"background-color: #4CAF50; color: #fff; padding: 10px 20px; border: none; border-radius: 5px;\">Pay Now</a>\r\n    <p>Our team at ConnectWise will always be available to assist you.</p>\r\n    <a href=\"{6}\" class=\"button\" style=\"background-color: #f44336; color: #fff; padding: 10px 20px; border: none; border-radius: 5px;\">ConnectWise</a>\r\n    <p>If you have any questions or concerns, please don't hesitate to contact us.</p>\r\n    <p class=\"footer\">Best regards,<br>ConnectWise Team</p>\r\n  </body>\r\n</html>\r\n", "Unregistered User Reminder", "Invoice || ConnectWise" },
                    { 4, "<!DOCTYPE html>\r\n            <html>\r\n                <head>\r\n                    <title>Invoice</title>\r\n                    <style>\r\n                        /* Define any styles you want to apply to your email here */\r\n                        body {{\r\n                            font-family: Arial, sans-serif;\r\n                            font-size: 16px;\r\n                            color: #333;\r\n                            line-height: 1.5;\r\n                        }}\r\n                        h1 {{\r\n                            font-size: 24px;\r\n                            margin-bottom: 10px;\r\n                        }}\r\n                        p {{\r\n                            margin-bottom: 20px;\r\n                        }}\r\n                        strong {{\r\n                            color: #007bff;\r\n                        }}\r\n                        .footer {{\r\n                            margin-top: 50px;\r\n                            font-size: 14px;\r\n                            color: #666;\r\n                        }}\r\n                        table{{\r\n                            border-collapse: collapse;\r\n                            width: 100%;\r\n                            border: 1px solid #000;\r\n                        }}\r\n                        th, td {{\r\n                            padding: 8px;\r\n                            text-align: left;\r\n                            border-bottom: 1px solid #000;\r\n                            border-right: 1px solid #000;\r\n                        }}\r\n                        th:last-child, td:last-child {{\r\n                            border-right: none;\r\n                        }}\r\n                    </style>\r\n                </head>\r\n                <body>\r\n                    <h1>Hello, sir/madam</h1>\r\n                    <p>This mail is been delivered to you by {0} for the invoice {1}</p>\r\n                    <p>The invoice {2} has a due date of {3}. Please make the complete payment before the due date; otherwise, a penalty will be charged.</p>\r\n                    <p>Our team at ConnectWise will always be present to help you.</p>\r\n                    <a href=\"{4}\" class=\"button\" style=\"background-color: #f44336; color: #fff; padding: 10px 20px; border: none; border-radius: 5px;\">ConnectWise</a>\r\n                    <p>If you have any questions or concerns, please don't hesitate to contact us.</p>\r\n                    <p class=\"footer\">Best regards,<br>ConnectWise Team</p>\r\n                </body>\r\n            </html>", "Invoice Reminder", "Invoice || ConnectWise" },
                    { 5, "\r\n        <!DOCTYPE html>\r\n        <html>\r\n            <head>\r\n                <title>Forgot Password</title>\r\n                <style>\r\n                    /* Define any styles you want to apply to your email here */\r\n                    body {{\r\n                        font-family: Arial, sans-serif;\r\n                        font-size: 16px;\r\n                        color: #333;\r\n                        line-height: 1.5;\r\n                    }}\r\n                    h1 {{\r\n                        font-size: 24px;\r\n                        margin-bottom: 10px;\r\n                    }}\r\n                    p {{\r\n                        margin-bottom: 20px;\r\n                    }}\r\n                    strong {{\r\n                        color: #007bff;\r\n                    }}\r\n                    .footer {{\r\n                        margin-top: 50px;\r\n                        font-size: 14px;\r\n                        color: #666;\r\n                    }}\r\n                    table {{\r\n                        border-collapse: collapse;\r\n                        width: 100%;\r\n                        border: 1px solid #000;\r\n                    }}\r\n                    th, td {{\r\n                        padding: 8px;\r\n                        text-align: left;\r\n                        border-bottom: 1px solid #000;\r\n                    }}\r\n                    th:last-child, td:last-child {{\r\n                        border-right: none;\r\n                    }}\r\n                </style>\r\n            </head>\r\n            <body>\r\n                <h1>Hello sir/madam,</h1>\r\n                <p>Follow the button below to change your password:</p>\r\n                <a href=\"{0}\" class=\"button\" style=\"background-color: #f44336; color: #fff; padding: 10px 20px; border: none; border-radius: 5px;\">Change Password</a>\r\n                <p>Our team at ConnectWise will always be present to help you.</p>\r\n                <a href=\"{1}\" class=\"button\" style=\"background-color: #f44336; color: #fff; padding: 10px 20px; border: none; border-radius: 5px;\">ConnectWise</a>\r\n                <p>If you have any questions or concerns, please don't hesitate to contact us.</p>\r\n                <p class=\"footer\">Best regards,<br>ConnectWise Team</p>\r\n            </body>\r\n        </html>", "Forgot Password", "Reset password" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[,]
                {
                    { 1, "Super Admin" },
                    { 2, "Account Admin" },
                    { 3, "User" }
                });

            migrationBuilder.InsertData(
                table: "Subscription",
                columns: new[] { "SubscriptionId", "Description", "IsActive", "SubscriptionFees", "TimePeriodInMonths" },
                values: new object[,]
                {
                    { 1, "Sub 1", true, 3000f, 1 },
                    { 2, "Sub 2", true, 5000f, 3 },
                    { 3, "Sub 3", true, 8000f, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Branch_CompanyId",
                table: "Branch",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_SubscriptionId",
                table: "Company",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_BranchId",
                table: "Invoice",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Reminder_InvoiceId",
                table: "Reminder",
                column: "InvoiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReminderLog_ReminderId",
                table: "ReminderLog",
                column: "ReminderId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_InvoiceId",
                table: "Transaction",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_BranchId",
                table: "Users",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MasterEmail");

            migrationBuilder.DropTable(
                name: "ReminderLog");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Reminder");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "Branch");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Subscription");
        }
    }
}
