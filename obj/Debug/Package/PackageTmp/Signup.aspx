<%@ Page Title="Înregistrare" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Inherits="Licenta.Signup"  CodeBehind="Signup.aspx.cs" %> 

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron" style="height: 494px">
       <%-- <h1>ASP.NET</h1>
        <p class="lead">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.</p>
        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>--%>
        <h3>Înregistrează-te</h3>
        <asp:createuserwizard id="Createuserwizard1" runat="server" Height="304px" Width="406px">
        <wizardsteps>
          <asp:createuserwizardstep runat="server" title="Sign Up for Your New Account">
              <ContentTemplate>
                  <table style="font-size:100%;height:304px;width:406px;">
                      <tr>
                          <td align="center" colspan="2">&nbsp;</td>
                      </tr>
                      <tr>
                          <td align="right">
                              <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                          </td>
                          <td>
                              <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                              <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Createuserwizard1">*</asp:RequiredFieldValidator>
                          </td>
                      </tr>
                      <tr>
                          <td align="right">
                              <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                          </td>
                          <td>
                              <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                              <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Createuserwizard1">*</asp:RequiredFieldValidator>
                          </td>
                      </tr>
                      <tr>
                          <td align="right">
                              <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">Confirm Password:</asp:Label>
                          </td>
                          <td>
                              <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                              <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword" ErrorMessage="Confirm Password is required." ToolTip="Confirm Password is required." ValidationGroup="Createuserwizard1">*</asp:RequiredFieldValidator>
                          </td>
                      </tr>
                      <tr>
                          <td align="right">
                              <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail:</asp:Label>
                          </td>
                          <td>
                              <asp:TextBox ID="Email" runat="server"></asp:TextBox>
                              <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email" ErrorMessage="E-mail is required." ToolTip="E-mail is required." ValidationGroup="Createuserwizard1">*</asp:RequiredFieldValidator>
                          </td>
                      </tr>
                      <tr>
                          <td align="right">
                              <asp:Label ID="QuestionLabel" runat="server" AssociatedControlID="Question">First Name:</asp:Label>
                          </td>
                          <td>
                              <asp:TextBox ID="Question" runat="server"></asp:TextBox>
                              <asp:RequiredFieldValidator ID="QuestionRequired" runat="server" ControlToValidate="Question" ErrorMessage="Security question is required." ToolTip="Security question is required." ValidationGroup="Createuserwizard1">*</asp:RequiredFieldValidator>
                          </td>
                      </tr>
                      <tr>
                          <td align="right">
                              <asp:Label ID="AnswerLabel" runat="server" AssociatedControlID="Answer">Last Name:</asp:Label>
                          </td>
                          <td>
                              <asp:TextBox ID="Answer" runat="server"></asp:TextBox>
                              <asp:RequiredFieldValidator ID="AnswerRequired" runat="server" ControlToValidate="Answer" ErrorMessage="Security answer is required." ToolTip="Security answer is required." ValidationGroup="Createuserwizard1">*</asp:RequiredFieldValidator>
                          </td>
                      </tr>
                      <tr>
                          <td align="center" colspan="2">
                              <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match." ValidationGroup="Createuserwizard1"></asp:CompareValidator>
                          </td>
                      </tr>
                      <tr>
                          <td align="center" colspan="2" style="color:Red;">
                              <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                          </td>
                      </tr>
                  </table>
              </ContentTemplate>
              <CustomNavigationTemplate>
                  <table border="0" cellspacing="5" style="width:100%;height:100%;">
                      <tr align="right">
                          <td align="right" colspan="0">
                              <asp:Button ID="StepNextButton" runat="server" CommandName="MoveNext" Text="Create User" ValidationGroup="Createuserwizard1" OnClick="StepNextButton_Click"/>
                          </td>
                      </tr>
                  </table>
              </CustomNavigationTemplate>
          </asp:createuserwizardstep>
          <asp:completewizardstep runat="server" title="Complete">
              <ContentTemplate>
                  <table style="font-size:100%;height:304px;width:406px;">
                      <tr>
                          <td align="center" colspan="2">Complete</td>
                      </tr>
                      <tr>
                          <td>Your account has been successfully created.</td>
                      </tr>
                      <tr>
                          <td align="right" colspan="2">
                              <asp:Button ID="ContinueButton" runat="server" CausesValidation="False" CommandName="Continue" Text="Continue" ValidationGroup="Createuserwizard1" />
                          </td>
                      </tr>
                  </table>
              </ContentTemplate>
          </asp:completewizardstep>
        </wizardsteps>
            <FinishNavigationTemplate>
                <asp:Button ID="FinishPreviousButton" runat="server" CausesValidation="False" CommandName="MovePrevious" Text="Previous" />
                <asp:Button ID="FinishButton" runat="server" CommandName="MoveComplete" Text="Finish" />
            </FinishNavigationTemplate>
      </asp:createuserwizard>
    </div>


</asp:Content>
