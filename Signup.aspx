<%@ Page Title="Înregistrare" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="Licenta._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
       <%-- <h1>ASP.NET</h1>
        <p class="lead">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.</p>
        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>--%>
        <h3>Înregistrează-te</h3>
        <asp:createuserwizard id="Createuserwizard1" runat="server">
        <wizardsteps>
          <asp:createuserwizardstep runat="server" title="Sign Up for Your New Account">
            <contenttemplate>
              <table border="0">
                <tr>
                  <td>
                    <table border="0" style="height: 100%; width: 100%;">
                      <tr>
                        <td align="center" colspan="2" style="height: 24px">
                          </td>
                      </tr>
                      <tr>
                        <td align="right" style="height: 35px">
                          <asp:label runat="server" associatedcontrolid="UserName" id="UserNameLabel">
                            User Name:</asp:label></td>
                        <td style="height: 35px">
                          <asp:textbox runat="server" id="UserName"></asp:textbox>
                          <asp:requiredfieldvalidator runat="server" controltovalidate="UserName" tooltip="User Name is required."
                            id="UserNameRequired" validationgroup="Createuserwizard1" errormessage="User Name is required.">
                            *</asp:requiredfieldvalidator>
                        </td>
                      </tr>
                      <tr>
                        <td align="right" style="height: 35px">
                          <asp:label runat="server" associatedcontrolid="Password" id="PasswordLabel">
                            Password:</asp:label></td>
                        <td style="height: 35px">
                          <asp:textbox runat="server" textmode="Password" id="Password"></asp:textbox>
                          <asp:requiredfieldvalidator runat="server" controltovalidate="Password" tooltip="Password is required."
                            id="PasswordRequired" validationgroup="Createuserwizard1" errormessage="Password is required.">
                            *</asp:requiredfieldvalidator>
                        </td>
                      </tr>
                      <tr>
                        <td align="right" style="height: 35px">
                          <asp:label runat="server" associatedcontrolid="ConfirmPassword" id="ConfirmPasswordLabel">
                            Confirm Password:</asp:label></td>
                        <td style="height: 35px">
                          <asp:textbox runat="server" textmode="Password" id="ConfirmPassword"></asp:textbox>
                          <asp:requiredfieldvalidator runat="server" controltovalidate="ConfirmPassword" tooltip="Confirm Password is required."
                            id="ConfirmPasswordRequired" validationgroup="Createuserwizard1" errormessage="Confirm Password is required.">
                            *</asp:requiredfieldvalidator>
                        </td>
                      </tr>
                      <tr>
                        <td align="right" style="height: 35px">
                          <asp:label runat="server" associatedcontrolid="Email" id="EmailLabel">
                            Email:</asp:label></td>
                        <td style="height: 35px">
                          <asp:textbox runat="server" id="Email"></asp:textbox>
                          <asp:requiredfieldvalidator runat="server" controltovalidate="Email" tooltip="Email is required."
                            id="EmailRequired" validationgroup="Createuserwizard1" errormessage="Email is required.">
                            *</asp:requiredfieldvalidator>
                        </td>
                      </tr>
                      <tr>
                        <td align="right" style="height: 35px">
                          <asp:label runat="server" associatedcontrolid="FirstName" id="FirstNameLabel">
                            First Name:</asp:label></td>
                        <td style="height: 35px">
                          <asp:textbox runat="server" id="FirstName"></asp:textbox>
                          <asp:requiredfieldvalidator runat="server" controltovalidate="FirstName" tooltip="First name is required."
                            id="FirstNameRequired" validationgroup="Createuserwizard1" errormessage="First name is required.">
                            *</asp:requiredfieldvalidator>
                        </td>
                      </tr>
                      <tr>
                        <td align="right" style="height: 35px">
                          <asp:label runat="server" associatedcontrolid="LastName" id="LastNameLabel">
                            Last Name:</asp:label></td>
                        <td style="height: 35px">
                          <asp:textbox runat="server" id="LastName"></asp:textbox>
                          <asp:requiredfieldvalidator runat="server" controltovalidate="LastName" tooltip="Last name is required."
                            id="LastNameRequired" validationgroup="Createuserwizard1" errormessage="Last name is required.">
                            *</asp:requiredfieldvalidator>
                        </td>
                      </tr>
                         <tr>
                <td align="right" style="height: 35px">
                    <asp:Label ID="QuestionLabel" runat="server" AssociatedControlID="Question" Visible="False">
                        Security Question:</asp:Label></td>
                <td style="height: 35px">
                    <asp:TextBox ID="Question" runat="server" Enabled="False" Visible="False"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="QuestionRequired" runat="server" ControlToValidate="Question"
                        ErrorMessage="Security question is required." ToolTip="Security question is required."
                        ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right" style="height: 35px">
                    <asp:Label ID="AnswerLabel" runat="server" AssociatedControlID="Answer" Visible="False">
                        Security Answer:</asp:Label></td>
                <td style="height: 35px">
                    <asp:TextBox ID="Answer" runat="server" Enabled="False" Visible="False"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="AnswerRequired" runat="server" ControlToValidate="Answer"
                        ErrorMessage="Security answer is required." ToolTip="Security answer is required."
                        ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                </td>
            </tr>
                      <tr>
                        <td align="center" colspan="2">
                          <asp:comparevalidator runat="server" display="Dynamic" errormessage="The Password and Confirmation Password must match."
                            controltocompare="ConfirmPassword" controltovalidate="Password" id="PasswordCompare"
                            validationgroup="Createuserwizard1">
                          </asp:comparevalidator>
                        </td>
                      </tr>
                      <tr>
                        <td align="center" colspan="2" style="color: Red;">
                          <asp:literal runat="server" enableviewstate="False" id="FailureText">
                          </asp:literal>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </table>
            </contenttemplate>
          </asp:createuserwizardstep>
          <asp:completewizardstep runat="server" title="Complete">
            <contenttemplate>
              <table border="0">
                <tr>
                  <td>
                    <table border="0" style="height: 100%; width: 100%;">
                      <tr>
                        <td align="center" colspan="2">
                          Complete</td>
                      </tr>
                      <tr>
                        <td>
                          Your account has been successfully created.</td>
                      </tr>
                      <tr>
                        <td align="right" colspan="2">
                          <asp:button runat="server" validationgroup="Createuserwizard1" commandname="Continue"
                            id="ContinueButton" causesvalidation="False" text="Continue" />
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </table>
            </contenttemplate>
          </asp:completewizardstep>
        </wizardsteps>
      </asp:createuserwizard>
    </div>


</asp:Content>
