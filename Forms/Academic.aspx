<%@ Page Language="C#" Title="Academic Application" MasterPageFile="/Forms/Common/Templates/Forms.Layout.Master" %>

<%@ MasterType  VirtualPath="/Forms/Common/Templates/Forms.Layout.Master" %>

<Script RunAt="Server">
/*===========================================================================================================================
| FORM: ACADEMIC APPLICATION
|
| Author        Katherine Trunkey, Ignia LLC (Katie@ignia.com)
| Client        GoldSim
| Project       Site Relaunch
|
| Purpose       Provides form template for GoldSim Academic Version application.
|
>============================================================================================================================
| Revisions     Date        Author                      Comments
| - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
|               11.24.08    Jeremy Caney                Initial version template.
|               07.27.10    Katherine Trunkey           Adapted for form template.
|               08.02.10    Jeremy Caney                Wired up event-handler for processing conditional email.
|               MM.DD.YY    FName LName                 Description
\--------------------------------------------------------------------------------------------------------------------------*/

/*=========================================================================================================================
| DECLARE PUBLIC PROPERTIES
>==========================================================================================================================
| Declare any public/global variables prior to page initiation; these will be treated as properties in the control
\------------------------------------------------------------------------------------------------------------------------*/

/*===========================================================================================================================
| PAGE LOAD
>============================================================================================================================
| Provide handling for functions that must run prior to page load.  This includes dynamically constructed controls.
\--------------------------------------------------------------------------------------------------------------------------*/
  void Page_Load(Object Src, EventArgs E) {

  /*-------------------------------------------------------------------------------------------------------------------------
  | SET MASTER PROPERTIES
  \------------------------------------------------------------------------------------------------------------------------*/
  //Associated topic for navigation -- sends to Forms.Layout.Master and then on to Page.Layout.master
    Master.FormTopic            =  Ignia.Topics.TopicRepository.RootTopic.GetTopic(739);
  //ContentType for form Topic
    Master.SaveAsContentType    = "AcademicRequest";
  //Submit button label
    Master.SubmitLabel          = "Send Request";
  //Submission email subject
    Master.EmailSubject         = "Academic Product Version Application";
    Master.EmailSender          = "website@goldsim.com"; // ((IgniaFormField)Email.FindControl("Email")).Value;
  //Redirect URL
    Master.SuccessUrl           = "/Topic/755/";


    }

/*===========================================================================================================================
| VALIDATOR: STUDENT SPONSOR
>============================================================================================================================
| Ensures that the student sponsor contact section is only required if the user selected "Student" affliation.
\--------------------------------------------------------------------------------------------------------------------------*/
  void StudentSponsorValidator(object source, ServerValidateEventArgs args) {
    if (AffiliationSelection.Items.FindByValue("Student").Selected) {
      Page.Validate("SponsorInformation");
      }
    else {
      args.IsValid = true;
      }
    }

/*===========================================================================================================================
| VALIDATOR: TERMS OF USE CHECK
>============================================================================================================================
| Ensures that the terms of use agreement checkbox is checked.
\--------------------------------------------------------------------------------------------------------------------------*/
  void TermsCheckValidator(object source, ServerValidateEventArgs args) {
    if (!TOUCheck.Checked) {
      args.IsValid = false;
      }
    }

</Script>

<asp:Content ContentPlaceHolderId="Content" runat="Server">

  <h2 class="Subtitle">GoldSim Academic Application Form</h2>
  <p>If you wish to apply for an Academic version of GoldSim, please fill out the application form provided below. The restrictions and terms for Academic licenses are described <a href="../Topic/26/">here</a>.</p>
  <p>Information regarding your Academic license will be sent from the software@goldsim.com email address. To ensure that you receive this communication, it is recommended that you add this address to your Safe Senders List.</p>
  <fieldset>
    <legend>Contact Information</legend>
  <em class="Instructions">For information regarding how we use your email address and other contact information, you can refer to our <a href="../Topic/613/">privacy policy</a>.</em>

    <%-- NAME BLOCK: FNAME, LNAME --%>
    <GoldSimForm:NameBlock ID="NameBlock" RunAt="Server" />

    <%-- EDUCATIONAL INSTITUTION --%>
    <GoldSimForm:Organization ID="Organization" LabelName="Name of Institution" AccessKey="I" RunAt="Server" />

    <%-- EMAIL --%>
    <GoldSimForm:Email ID="Email" RunAt="Server" />
    <em class="Instructions">Email must be associated with an academic institution</em>

    <%-- DEPARTMENT --%>
    <Ignia:FormField   ID = "Department"
      LabelName           = "Department"
      AccessKey           = "D"
      MaxLength           = "150"
      FieldSize           = "320"
      Required            = "True"
      CssClass            = "TextField"
      SkinId              = "BoxedPairs"
      RunAt               = "Server"
      />

    <%-- PHONE --%>
    <GoldSimForm:Phone ID="Phone" RunAt="Server" />

    <%-- ADDRESS BLOCK: ADDRESS1, ADDRESS2, CITY, STATE/PROVINCE, ZIP/POSTAL --%>
    <GoldSimForm:AddressBlock ID="Address" RunAt="Server" />

    <%-- COUNTRY SELECTION --%>
    <GoldSimForm:CountrySelection ID="Country" RunAt="Server" />

    <%-- AFFILIATION --%>
    <div class="FieldContainer">
      <label for="AffiliationSelection" accesskey="A" RunAt="Server"><strong>Affiliation</strong></label>
      <asp:DropDownList ID="AffiliationSelection" runat="server">
        <asp:ListItem Value="Professor">Professor</asp:ListItem>
        <asp:ListItem Value="Teacher">Teacher</asp:ListItem>
        <asp:ListItem Value="Researcher">Researcher</asp:ListItem>
        <asp:ListItem Value="Student">Student</asp:ListItem>
      </asp:DropDownList>
      <asp:RequiredFieldValidator ControlToValidate="AffiliationSelection" RunAt="Server" />
      <asp:CustomValidator
        ControlToValidate       = "AffiliationSelection"
        OnServerValidate        = "StudentSponsorValidator"
        ErrorMessage            = "Students: please enter your sponsorship contact information."
        Display                 = "None"
        RunAt                   = "Server"
        />
    </div>

    <%-- AREA OF FOCUS SELECTION --%>
    <GoldSimForm:AreaOfFocusSelection ID="ContactFocus" RunAt="Server" />

   <br>

    <%-- AFFILIATION: STUDENT SPONSOR --%>
    <fieldset ID="SponsorInformation">
      <legend>Students</legend>
      <p>Please identify a professor or teacher who will act as your sponsor.</p>
      <%-- SPONSOR FIRST NAME --%>
      <Ignia:FormField   ID = "SponsorFirstName"
        LabelName           = "Sponsor First Name"
        AccessKey           = "F"
        MaxLength           = "150"
        FieldSize           = "320"
        Required            = "True"
        ValidationGroup     = "SponsorInformation"
        CssClass            = "TextField"
        SkinId              = "BoxedPairs"
        RunAt               = "Server"
        />
      <%-- SPONSOR LAST NAME --%>
      <Ignia:FormField   ID = "SponsorLastName"
        LabelName           = "Sponsor Last Name"
        AccessKey           = "L"
        MaxLength           = "150"
        FieldSize           = "320"
        Required            = "True"
        ValidationGroup     = "SponsorInformation"
        CssClass            = "TextField"
        SkinId              = "BoxedPairs"
        RunAt               = "Server"
        />
      <%-- SPONSOR DEPARTMENT --%>
      <Ignia:FormField   ID = "SponsorDepartment"
        LabelName           = "Sponsor Department"
        AccessKey           = "D"
        MaxLength           = "150"
        FieldSize           = "320"
        Required            = "True"
        ValidationGroup     = "SponsorInformation"
        CssClass            = "TextField"
        SkinId              = "BoxedPairs"
        RunAt               = "Server"
        />
      <%-- SPONSOR EMAIL --%>
      <Ignia:FormField   ID = "SponsorEmail"
        LabelName           = "Sponsor Email"
        AccessKey           = "E"
        MaxLength           = "150"
        FieldSize           = "320"
        Required            = "True"
        ValidationGroup     = "SponsorInformation"
        CssClass            = "TextField"
        SkinId              = "BoxedPairs"
        RunAt               = "Server"
        />
      <%-- SPONSOR PHONE --%>
      <Ignia:FormField   ID = "SponsorPhone"
        LabelName           = "Sponsor Telephone"
        AccessKey           = "T"
        MaxLength           = "150"
        FieldSize           = "320"
        Required            = "True"
        ValidationGroup     = "SponsorInformation"
        CssClass            = "TextField"
        SkinId              = "BoxedPairs"
        RunAt               = "Server"
        />
    </fieldset>


  </fieldset>

  <fieldset>
    <legend>Additional Information</legend>

    <%-- MODULE INTEREST CHECKBOXES --%>
    <GoldSimForm:ModuleInterestSelection ID="ModuleInterest" RunAt="Server" />

    <%-- SIMULATION PROBLEM DESCRIPTION --%>
    <Ignia:FormField   ID = "ProblemDescription"
      LabelName           = "What problem are you trying to solve?"
      AccessKey           = "p"
      MaxLength           = "150"
      FieldSize           = "468"
      Required            = "True"
      TextMode            = "MultiLine"
      CssClass            = "BlockLabel TextField"
      SkinId              = "BoxedPairs"
      RunAt               = "Server"
      />

    <div class="FieldContainer Referral Select">
      <asp:DropDownList ID="ReferralSelectionList" runat="server">
        <asp:ListItem Value="" Selected="True">How did you learn about GoldSim?</asp:ListItem>
        <asp:ListItem Value="Google">Google</asp:ListItem>
        <asp:ListItem Value="Other Search Engine">Other Search Engine</asp:ListItem>
        <asp:ListItem Value="Wikipedia">Wikipedia</asp:ListItem>
        <asp:ListItem Value="Word of Mouth">Word Of Mouth</asp:ListItem>
        <asp:ListItem Value="From a Colleague">From a Colleague</asp:ListItem>
        <asp:ListItem Value="Trade Show">Trade Show</asp:ListItem>
        <asp:ListItem Value="Journal or Advertisement">Journal or Advertisement</asp:ListItem>
        <asp:ListItem Value="Link from another website">Link from another website</asp:ListItem>
        <asp:ListItem Value="Other">Other</asp:ListItem>
      </asp:DropDownList>
      <asp:RequiredFieldValidator
        ControlToValidate       = "ReferralSelectionList"
        RunAt                   = "Server"
      />
    </div>
    <Ignia:FormField   ID = "ReferralDetails"
      LabelName           = "Referral Details"
      AccessKey           = "D"
      MaxLength           = "150"
      FieldSize           = "320"
      CssClass            = "TextField"
      SkinId              = "BoxedPairs"
      RunAt               = "Server"
      />
    <em class="Instructions">Please provide additional details for other search engines, journal name, specific trade show, etc.</em>

  </fieldset>

  <fieldset>
    <legend>Terms of Use</legend>

    <p>By checking the box below I agree to the following:</p>
    <ol>
      <li>I will only use GoldSim Academic for teaching and/or "internally funded" graduate student research (i.e., student research funded at a low-level, typically using departmental funds) at a non-profit academic institution.</li>
      <li>I will not use GoldSim Academic for "externally funded" research (i.e., sponsored research projects) at an academic institution.</li>
      <li>I will not use GoldSim Academic for sponsored research at a private or government research institution.</li>
      <li>I will not use GoldSim Academic for consulting or other commercial purposes.</li>
      <li>If GoldSim Academic is used to generate results that are published, I will notify the GoldSim Technology Group and acknowledge the GoldSim Technology Group in the publication.</li>
      <li>Create a link to http://www.goldsim.com, anchored to the text phrase "simulation software", if actively using GoldSim in your teaching or research.</li>
     </ol>
    <div class="FieldContainer">
      <label for="TOUCheck" RunAt="Server">I agree to these terms of use.</label>
      <asp:CheckBox ID="TOUCheck" RunAt="Server" />
      <asp:CustomValidator
        OnServerValidate        = "TermsCheckValidator"
        ErrorMessage            = "Please accept the terms of use."
        Display                 = "None"
        RunAt                   = "Server"
        />
    </div>
  </fieldset>

</asp:Content>