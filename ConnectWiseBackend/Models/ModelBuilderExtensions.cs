using Microsoft.AspNetCore.Components.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.SqlServer.Server;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Policy;
using System;

namespace ConnectWiseBackend.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
            new Role() { RoleId = 1, RoleName = "Super Admin" },
            new Role() { RoleId = 2, RoleName = "Account Admin" },
            new Role() { RoleId = 3, RoleName = "User" }
            );

            modelBuilder.Entity<Subscription>().HasData(
            new Subscription() { SubscriptionId = 1, Description = "Sub 1", SubscriptionFees = 3000, TimePeriodInMonths = 1, IsActive = true },
            new Subscription() { SubscriptionId = 2, Description = "Sub 2", SubscriptionFees = 5000, TimePeriodInMonths = 3, IsActive = true },
            new Subscription() { SubscriptionId = 3, Description = "Sub 3", SubscriptionFees = 8000, TimePeriodInMonths = 1, IsActive = true }
            );

            modelBuilder.Entity<MasterEmail>().HasData(
                new MasterEmail()
                {
                    MasterEmailId = 1,
                    EmailName = "Company Registration",
                    EmailSubject = "Company registered successfully",
                    EmailBody = @"<!DOCTYPE html>
<html>
  <head>
    <title>Company Registration</title>
    <style>
      /* Define any styles you want to apply to your email here */
      body {{
        font-family: Arial, sans-serif;
        font-size: 16px;
        color: #333;
        line-height: 1.5;
      }}
      h1 {{
        font-size: 24px;
        margin-bottom: 10px;
      }}
      p {{
        margin-bottom: 20px;
      }}
      strong {{
        color: #007bff;
      }}
      .footer {{
        margin-top: 50px;
        font-size: 14px;
        color: #666;
      }}
    </style>
  </head>
  <body>
    <h1>Hello, {0}'s Admin</h1>

    <p>Thank you for signing up for our service. Your company has been successfully registered with ConnectWise.</p>

    <p>Our Team at ConnectWise will always be available to assist you.</p>
    <a href=""{1}"" class=""button"" style=""background-color: #f44336; color: #fff; padding: 10px 20px; border: none; border-radius: 5px;"">ConnectWise</a>

    <p>If you have any questions or concerns, please don't hesitate to contact us.</p>

    <p class=""footer"">Best regards,<br>ConnectWise Team</p>
  </body>
</html>
"
                },
                new MasterEmail()
                {
                    MasterEmailId = 2,
                    EmailName = "User Registration",
                    EmailSubject = "User registered successfully",
                    EmailBody = @"<!DOCTYPE html>
 <html>
  <head>
    <title>User Registration</title>
    <style>
      /* Define any styles you want to apply to your email here */
      body {{
        font-family: Arial, sans-serif;
        font-size: 16px;
        color: #333;
        line-height: 1.5;
      }}
      h1 {{
        font-size: 24px;
        margin-bottom: 10px;
      }}
      p {{
        margin-bottom: 20px;
      }}
      strong {{
        color: #007bff;
      }}
      .footer {{
        margin-top: 50px;
        font-size: 14px;
        color: #666;
      }}
      table {{
        border-collapse: collapse;
        width: 100%;
        border: 1px solid #000;
      }}
      th, td {{
        padding: 8px;
        text-align: left;
        border-bottom: 1px solid #000;
        border-right: 1px solid #000;
      }}
      th:last-child, td:last-child {{
        border-right: none;
      }}
    </style>
  </head>
  <body>
    <h1>Hello, sir/madam</h1>
    <p>Congratulations! You have been successfully registered on our application by your company {0}. Your username is {1} and password is {2}. We kindly request you to change your password using the 'Forgot Password' option.</p>

    <p>Our Team at ConnectWise will always be available to assist you</p>
    <a href=""{3}"" class=""button"" style=""background-color: #f44336; color: #fff; padding: 10px 20px; border: none; border-radius: 5px;"""">ConnectWise</a>

    <p>If you have any questions or concerns, please don't hesitate to contact us.</p>

    <p class=""footer"">Best regards,<br>ConnectWise Team</p>
  </body>
</html>"
                },
                new MasterEmail()
                {
                    MasterEmailId = 3,
                    EmailName = "Unregistered User Reminder",
                    EmailSubject = "Invoice || ConnectWise",
                    EmailBody = @"<!DOCTYPE html>

<html>
  <head>
    <title>Reminder</title>
    <style>
      /* Define any styles you want to apply to your email here */
      body {{
        font-family: Arial, sans-serif;
        font-size: 16px;
        color: #333;
        line-height: 1.5;
      }}
      h1 {{
        font-size: 24px;
        margin-bottom: 10px;
      }}
      p {{
        margin-bottom: 20px;
      }}
      strong {{
        color: #007bff;
      }}
      .footer {{
        margin-top: 50px;
        font-size: 14px;
        color: #666;
      }}
      table {{
        border-collapse: collapse;
        width: 100%;
        border: 1px solid #000;
      }}
      th, td {{
        padding: 8px;
        text-align: left;
        border-bottom: 1px solid #000;
        border-right: 1px solid #000;
      }}
      th:last-child, td:last-child {{
        border-right: none;
      }}
    </style>
  </head>
  <body>
    <h1>Hello, sir/madam</h1>
    <p>This email is being delivered to you by {0} for the invoice {1}</p>
    <p>Below, you will find a Pay Button which will redirect you to the payment gateway, where you will be able to pay for the invoice:</p>
    <table>
      <tr>
        <th>Invoice</th>
        <th></th>
      </tr>
      <tr>
        <td>Name</td>
        <td>{2}</td>
      </tr>
      <tr>
        <td>Total Amount</td>
        <td>{3}</td>
      </tr>
      <tr>
        <td>Due Amount</td>
        <td>{4}</td>
      </tr>
    </table>
    <a href=""{5}"" class=""button"" style=""background-color: #4CAF50; color: #fff; padding: 10px 20px; border: none; border-radius: 5px;"">Pay Now</a>
    <p>Our team at ConnectWise will always be available to assist you.</p>
    <a href=""{6}"" class=""button"" style=""background-color: #f44336; color: #fff; padding: 10px 20px; border: none; border-radius: 5px;"">ConnectWise</a>
    <p>If you have any questions or concerns, please don't hesitate to contact us.</p>
    <p class=""footer"">Best regards,<br>ConnectWise Team</p>
  </body>
</html>
"
                },
                new MasterEmail() { MasterEmailId = 4, EmailName = "Invoice Reminder", EmailSubject = "Invoice || ConnectWise", EmailBody = @"<!DOCTYPE html>
            <html>
                <head>
                    <title>Invoice</title>
                    <style>
                        /* Define any styles you want to apply to your email here */
                        body {{
                            font-family: Arial, sans-serif;
                            font-size: 16px;
                            color: #333;
                            line-height: 1.5;
                        }}
                        h1 {{
                            font-size: 24px;
                            margin-bottom: 10px;
                        }}
                        p {{
                            margin-bottom: 20px;
                        }}
                        strong {{
                            color: #007bff;
                        }}
                        .footer {{
                            margin-top: 50px;
                            font-size: 14px;
                            color: #666;
                        }}
                        table{{
                            border-collapse: collapse;
                            width: 100%;
                            border: 1px solid #000;
                        }}
                        th, td {{
                            padding: 8px;
                            text-align: left;
                            border-bottom: 1px solid #000;
                            border-right: 1px solid #000;
                        }}
                        th:last-child, td:last-child {{
                            border-right: none;
                        }}
                    </style>
                </head>
                <body>
                    <h1>Hello, sir/madam</h1>
                    <p>This mail is been delivered to you by {0} for the invoice {1}</p>
                    <p>The invoice {2} has a due date of {3}. Please make the complete payment before the due date; otherwise, a penalty will be charged.</p>
                    <p>Our team at ConnectWise will always be present to help you.</p>
                    <a href=""{4}"" class=""button"" style=""background-color: #f44336; color: #fff; padding: 10px 20px; border: none; border-radius: 5px;"">ConnectWise</a>
                    <p>If you have any questions or concerns, please don't hesitate to contact us.</p>
                    <p class=""footer"">Best regards,<br>ConnectWise Team</p>
                </body>
            </html>" },
                new MasterEmail() { MasterEmailId = 5, EmailName = "Forgot Password", EmailSubject = "Reset password", 
                    EmailBody = @"
        <!DOCTYPE html>
        <html>
            <head>
                <title>Forgot Password</title>
                <style>
                    /* Define any styles you want to apply to your email here */
                    body {{
                        font-family: Arial, sans-serif;
                        font-size: 16px;
                        color: #333;
                        line-height: 1.5;
                    }}
                    h1 {{
                        font-size: 24px;
                        margin-bottom: 10px;
                    }}
                    p {{
                        margin-bottom: 20px;
                    }}
                    strong {{
                        color: #007bff;
                    }}
                    .footer {{
                        margin-top: 50px;
                        font-size: 14px;
                        color: #666;
                    }}
                    table {{
                        border-collapse: collapse;
                        width: 100%;
                        border: 1px solid #000;
                    }}
                    th, td {{
                        padding: 8px;
                        text-align: left;
                        border-bottom: 1px solid #000;
                    }}
                    th:last-child, td:last-child {{
                        border-right: none;
                    }}
                </style>
            </head>
            <body>
                <h1>Hello sir/madam,</h1>
                <p>Follow the button below to change your password:</p>
                <a href=""{0}"" class=""button"" style=""background-color: #f44336; color: #fff; padding: 10px 20px; border: none; border-radius: 5px;"">Change Password</a>
                <p>Our team at ConnectWise will always be present to help you.</p>
                <a href=""{1}"" class=""button"" style=""background-color: #f44336; color: #fff; padding: 10px 20px; border: none; border-radius: 5px;"">ConnectWise</a>
                <p>If you have any questions or concerns, please don't hesitate to contact us.</p>
                <p class=""footer"">Best regards,<br>ConnectWise Team</p>
            </body>
        </html>"
                }
                );

        }
    }
}
