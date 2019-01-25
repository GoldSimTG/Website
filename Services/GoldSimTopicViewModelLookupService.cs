﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using System;
using GoldSim.Web.Models;

namespace GoldSim.Web {

  /*============================================================================================================================
  | CLASS: GOLDSIM TOPIC VIEW MODEL LOOKUP SERVICE
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a mapping between string and class names to be used when mapping <see cref="Topic"/> to a <see
  ///   cref="TopicViewModel"/> or derived class.
  /// </summary>
  public class GoldSimTopicViewModelLookupService : Ignia.Topics.ViewModels.TopicViewModelLookupService {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Instantiates a new instance of the <see cref="GoldSimViewModelLookupService"/>.
    /// </summary>
    /// <returns>A new instance of the <see cref="GoldSimViewModelLookupService"/>.</returns>
    internal GoldSimTopicViewModelLookupService() : base() {

      /*------------------------------------------------------------------------------------------------------------------------
      | Add GoldSim specific view models
      \-----------------------------------------------------------------------------------------------------------------------*/
      Add(typeof(ApplicationBasePageTopicViewModel));
      Add(typeof(ApplicationContainerTopicViewModel));
      Add(typeof(ApplicationIndexTopicViewModel));
      Add(typeof(ApplicationPageTopicViewModel));
      Add(typeof(DocumentPointerTopicViewModel));
      Add(typeof(ExampleApplicationTopicViewModel));
      Add(typeof(ExampleIndexTopicViewModel));
      Add(typeof(FaqItemTopicViewModel));
      Add(typeof(FaqTopicViewModel));
      Add(typeof(GlossaryItemTopicViewModel));
      Add(typeof(GlossaryTopicViewModel));
      Add(typeof(HomeTopicViewModel));
      Add(typeof(ModulePageTopicViewModel));
      Add(typeof(PaymentsTopicViewModel));
      Add(typeof(SearchTopicViewModel));
      Add(typeof(TechnicalPaperListTopicViewModel));
      Add(typeof(TechnicalPaperTopicViewModel));

      /*------------------------------------------------------------------------------------------------------------------------
      | Override Ignia topics
      \-----------------------------------------------------------------------------------------------------------------------*/
      Replace(typeof(NavigationTopicViewModel));
      Replace(typeof(PageGroupTopicViewModel));

      /*------------------------------------------------------------------------------------------------------------------------
      | Function: Replace
      \-----------------------------------------------------------------------------------------------------------------------*/
      void Replace(Type type) {
        if (Contains(type.Name)) {
          Remove(type.Name);
        }
        Add(type);
      }

    }

  } //Class
} //Namespace