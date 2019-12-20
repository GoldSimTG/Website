﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using System;
using System.ComponentModel.DataAnnotations;
using GoldSim.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace GoldSim.Web.Models.Forms.BindingModels {

  /*============================================================================================================================
  | BINDING MODEL: PAYMENT FORM
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed binding model representing the pay invoice form.
  /// </summary>
  public class PaymentFormBindingModel {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Initializes a new instance of a <see cref="PaymentFormBindingModel"/> object.
    /// </summary>
    public PaymentFormBindingModel() : base() {
    }

    /*==========================================================================================================================
    | PROPERTY: CARDHOLDER NAME
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the name on the credit card being paid with.
    /// </summary>
    [Required]
    [Display(Name="Cardholder Name")]
    public string CardholderName { get; set; }

    /*==========================================================================================================================
    | PROPERTY: ORGANIZATION
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the user's organization or institution name.
    /// </summary>
    [Required]
    [StringLength(255)]
    [Display(Name = "Organization Name")]
    public virtual string Organization { get; set; }

    /*==========================================================================================================================
    | PROPERTY: EMAIL ADDRESS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Gets or sets the user's email address.
    /// </summary>
    [Required]
    [EmailAddress]
    [Display(Name = "Email Address")]
    public virtual string Email { get; set; }

    /*==========================================================================================================================
    | INVOICE NUMBER
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The unique identifier generated by GoldSim's invoicing software and used to track payments back to purchases.
    /// </summary>
    [Required]
    [Range(1000, 99999)]
    [Display(Name="Invoice Number")]
    [Remote(nameof(PaymentsController.VerifyInvoiceNumber), "Payments")]
    public int InvoiceNumber { get; set; }

    /*==========================================================================================================================
    | INVOICE AMOUNT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The amount due on the invoice, in United States Dollars (USD).
    /// </summary>
    [Required]
    [Range(1.00, 1000000.00)]
    [Display(Name = "Invoice Amount")]
    [Remote(nameof(PaymentsController.VerifyInvoiceAmount), "Payments", AdditionalFields=nameof(InvoiceNumber))]
    public double InvoiceAmount { get; set; }

    /*==========================================================================================================================
    | PROPERTY: PAYMENT METHOD NONCE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   The encoded payment information returned from the Braintree script.
    /// </summary>
    /// <remarks>
    ///   The credit card number, expiration date, and CCV are all collected as part of the form, but not posted to the server.
    ///   Instead, the data is encrypted in transit, and submitted as the <see cref="PaymentMethodNonce"/>. Even though the
    ///   connection itself is encrypted via transport level security (TLS), encrypting the data itself ensures that it won't
    ///   inadvertantly get stored in clear text as part of e.g. log files. For this reason, this binding model doesn't have
    ///   fields for the credit card information, instead just exposing this property. If this value is null, that effectively
    ///   means that no credit card information was entered or validated. That should get caught by Braintree's validation
    ///   script, but to be safe we're marking this field as required.
    /// </remarks>
    [Required(ErrorMessage="The credit card information is required.")]
    public string PaymentMethodNonce { get; set; }

  } //Class

} //Namespace