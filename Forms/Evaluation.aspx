<%@ Page Language="C#" Title="GoldSim Trial" %>

<%@ MasterType  VirtualPath="/Forms/Common/Templates/Forms.Layout.Master" %>
<%@ Reference   Control="/Common/Global/Controls/FormField.ascx" %>

<Script RunAt="Server">
/*==============================================================================================================================
| FORM: EVALUATION REQUEST
|
| Author        Katherine Trunkey, Ignia LLC (Katie@ignia.com)
| Client        GoldSim
| Project       Site Relaunch
|
| Purpose       Provides form template for GoldSim software evaluation request.
|
>===============================================================================================================================
| Revisions     Date        Author                      Comments
| - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
|               11.24.08    Jeremy Caney                Initial version template.
|               07.27.10    Katherine Trunkey           Adapted for form template.
|               08.02.10    Jeremy Caney                Wired up event-handler for processing conditional email.
|               MM.DD.YY    FName LName                 Description
\-----------------------------------------------------------------------------------------------------------------------------*/

  /*============================================================================================================================
  | PAGE LOAD
  \---------------------------------------------------------------------------------------------------------------------------*/
  void Page_Load(Object Src, EventArgs E) {

    /*--------------------------------------------------------------------------------------------------------------------------
    | SET MASTER PROPERTIES
    \-------------------------------------------------------------------------------------------------------------------------*/
    // Associated topic for navigation -- sends to Forms.Layout.Master and then on to Page.Layout.master
    // Master.FormTopic         =  Ignia.Topics.TopicRepository.RootTopic.GetTopic(460);

    // ContentType for the form Topic
    Master.SaveAsContentType    = "EvaluationRequest";

    // Submit button label
    Master.SubmitLabel          = "Request Evaluation";

    // Custom processing event
    Master.ProcessForm         += ProcessForm;

  }

  /*============================================================================================================================
  | VALIDATOR: EVALUATION METHOD
  >=============================================================================================================================
  | Ensures that at least one of the evaluation methods is selected.
  \---------------------------------------------------------------------------------------------------------------------------*/
  void EvaluationMethodValidator(object source, ServerValidateEventArgs args) {
    args.IsValid = !String.IsNullOrEmpty(EvaluationTypeList.SelectedValue);
  }

  /*============================================================================================================================
  | PROCESS FORM
  >=============================================================================================================================
  | Override default form processing to provide conditional email support.  Includes a manual redirect to prevent default
  | processing of the form since the evaluation requires a highly customized experience.
  \---------------------------------------------------------------------------------------------------------------------------*/
  public void ProcessForm(Object sender, CommandEventArgs args) {

    /*--------------------------------------------------------------------------------------------------------------------------
    | Local variables
    \-------------------------------------------------------------------------------------------------------------------------*/
    bool        isTrial         = EvaluationTypeList.Items.FindByValue("Trial").Selected;
    bool        isDemo          = EvaluationTypeList.Items.FindByValue("Demo").Selected;
    string      email           = ((IgniaFormField)Email.FindControl("Email")).Value;

    /*--------------------------------------------------------------------------------------------------------------------------
    | Send trial request email
    \-------------------------------------------------------------------------------------------------------------------------*/
    if (isTrial) {
      Master.EmailForm("Evaluation Download Request", "Software@GoldSim.com", "website@goldsim.com");
    }

    /*--------------------------------------------------------------------------------------------------------------------------
    | Save form as Topic
    \-------------------------------------------------------------------------------------------------------------------------*/
    Master.SaveFormAsTopic();

    /*--------------------------------------------------------------------------------------------------------------------------
    | Send demo email
    \-------------------------------------------------------------------------------------------------------------------------*/
    if (isDemo) {
      // Customize the subject line based on whether a trial was also selected
      string demoSubject = "Demo request WITH" + (isTrial? "" : "OUT") + " Evaluation Request";
      Master.EmailForm(demoSubject, "Software@GoldSim.com", "website@goldsim.com");
    }

    /*--------------------------------------------------------------------------------------------------------------------------
    | Send email receipt (to user)
    \-------------------------------------------------------------------------------------------------------------------------*/
    Utility.SendWebPage(
      "http://" + Request.Url.Host + "/Forms/Evaluation.Receipt.html",
      "GoldSim Evaluation Request",
      "Software@GoldSim.com",
      email
    );

    /*--------------------------------------------------------------------------------------------------------------------------
    | Redirect user
    \-------------------------------------------------------------------------------------------------------------------------*/
    Response.Redirect("/Topic/431/");
    Response.End();

  }

</Script>

<asp:Content ContentPlaceHolderID="Subtitle" runat="server">
  <p class="subtitle">Request Free GoldSim Trial Version</p>
</asp:Content>

<asp:Content ContentPlaceHolderId="Content" runat="Server">
  <p>Trial versions are fully functional expiring 30 days after registration. GoldSim is easy to install, register (and uninstall), and, of course, there is no obligation to buy and your privacy is respected. Free technical support is provided throughout your trial.</p>

  <fieldset style="display: none;">
    <legend>Evaluation Method</legend>

    <%-- EVALUATION METHOD CHECKBOXES --%>
    <div class="FieldContainer Checkboxes">
      <label For="EvaluationTypeList" RunAt="Server">How would you like to evaluate GoldSim?</label>
      (Please select at least one option.)
      <asp:CheckBoxList ID="EvaluationTypeList" RepeatLayout="Flow" RunAt="server">
        <asp:ListItem Value="Trial" Selected="true">GoldSim Trial Version</asp:ListItem>
        <asp:ListItem Value="Demo">Live Demonstration</asp:ListItem>
      </asp:CheckBoxList>
      <asp:CustomValidator
        ControlToValidate       = "Email:Email:Field"
        OnServerValidate        = "EvaluationMethodValidator"
        ErrorMessage            = "You must select at least one evaluation method."
        RunAt                   = "Server"
        />
    </div>

  </fieldset>

  <fieldset>
    <div class="grid-x grid-margin-x">

      <%-- NAME BLOCK: FNAME, LNAME --%>
      <GoldSimForm:NameBlock ID="NameBlock" RunAt="Server" />

      <%-- ORGANIZATION --%>
      <!-- Organization -->
      <div class="medium-6 cell">
        <GoldSimForm:Organization ID="Organization" RunAt="Server" />
      </div>
      <!-- Organization -->

      <%-- EMAIL --%>
      <!-- Email -->
      <div class="medium-6 cell">
        <GoldSimForm:Email ID="Email" RunAt="Server" />
      </div>
      <div class="cell">
        <p class="field instructions">Only institutional email domains are accepted. Email addresses of free domains (yahoo.com, gmail.com, etc.) are not accepted nor processed. You can refer to our <a href="/Topic/4222/">privacy policy</a> regarding how we use your email address.</p>
      </div>
      <!-- /Email -->

      <%-- COUNTRY SELECTION --%>
      <!-- Country -->
      <div class="medium-6 cell">
        <GoldSimForm:CountrySelection ID="Country" IsRequired="true" RunAt="Server" />
      </div>
      <!-- /Country -->

      <%-- PHONE --%>
      <!-- Phone -->
      <div class="medium-6 cell">
        <GoldSimForm:Phone ID="Phone" RunAt="Server" />
      </div>
      <!-- /Phone -->

      <%-- AREA OF FOCUS SELECTION --%>
      <GoldSimForm:AreaOfFocusSelection ID="ContactFocus" RunAt="Server" />

      <%-- SIMULATION PROBLEM DESCRIPTION --%>
      <!-- Problem Description -->
      <div class="cell">
        <Ignia:FormField     ID = "ProblemDescription"
          LabelName             = "*What problem are you trying to solve?"
          AccessKey             = "p"
          MaxLength             = "150"
          FieldSize             = "464"
          Required              = "True"
          TextMode              = "MultiLine"
          CssClass              = "BlockLabel TextField"
          SkinId                = "BoxedPairs"
          RunAt                 = "Server"
          />
      </div>
      <!-- /Problem Description -->

      <%-- EXISTING SOFTWARE/TOOLS DESCRIPTION --%>
      <!-- Existing Tools Description -->
      <div class="cell">
        <Ignia:FormField     ID = "ExistingToolsDescription"
          LabelName             = "*What other risk analysis tools do you use, or are evaluating?"
          AccessKey             = "t"
          MaxLength             = "150"
          FieldSize             = "464"
          Required              = "True"
          TextMode              = "MultiLine"
          CssClass              = "BlockLabel TextField"
          SkinId                = "BoxedPairs"
          RunAt                 = "Server"
          />
      </div>
      <!-- /Existing Tools Description -->

      <%-- MODULE INTEREST CHECKBOXES --%>
      <GoldSimForm:ModuleInterestSelection ID="ModuleInterest" RunAt="Server" />

      <%-- REFERRAL SOURCE SELECTION --%>
      <GoldSimForm:ReferralSourceSelection ID="ReferralSource" RunAt="Server" />

    </div>

  </fieldset>

</asp:Content>