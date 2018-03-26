﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using Ignia.Topics.ViewModels;
using Ignia.Topics.Mapping;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;

namespace GoldSim.Web.Models {

  /*============================================================================================================================
  | VIEW MODEL: APPLICATION INDEX TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a <c>ApplicationIndex</c> topic.
  /// </summary>
  public class ApplicationIndexTopicViewModel : PageTopicViewModel {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    bool                        _isFirst                        = true;

    /*==========================================================================================================================
    | GENERIC PROPERTIES
    \-------------------------------------------------------------------------------------------------------------------------*/
    public string FilteredDocumentType { get; set; }
    [Metadata("ApplicationCategories")]
    public TopicViewModelCollection<LookupListItemTopicViewModel> Categories { get; set; }
    public TopicViewModelCollection<ApplicationBasePageTopicViewModel> EnvironmentalSystems { get; set; }
    public TopicViewModelCollection<ApplicationBasePageTopicViewModel> BusinessSystems { get; set; }
    public TopicViewModelCollection<ApplicationBasePageTopicViewModel> EngineeredSystems { get; set; }

    /*==========================================================================================================================
    | GET CATEGORY TITLE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Looks up a category from the <see cref="Categories"/> collection based on the <see
    ///   cref="LookupListItemTopicViewModel.Key"/> and returns the corresponding <see
    ///   cref="LookupListItemTopicViewModel.Title"/>.
    /// </summary>
    /// <param name="category"></param>
    /// <returns>The title corresponding to the category key.</returns>
    public string GetCategoryTitle(string category) {
      return Categories.Where(t => t.Key.Equals(category)).FirstOrDefault().Title;
    }

    /*==========================================================================================================================
    | GET ALL APPLICATIONS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Returns a consolidated list of <i>all</i> applications from the corresponding properties.
    /// </summary>
    /// <returns>A consolidated list of applications.</returns>
    public TopicViewModelCollection<ApplicationBasePageTopicViewModel> GetAllApplications() {
      return new TopicViewModelCollection<ApplicationBasePageTopicViewModel>(
        EnvironmentalSystems.Concat(BusinessSystems).Concat(EngineeredSystems).Distinct().ToList()
      );
    }

    /*==========================================================================================================================
    | GET CATEGORIZED APPLICATIONS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Returns a dictionary of applications keyed by <see cref="Categories"/>.
    /// </summary>
    /// <returns>A consolidated list of applications.</returns>
    public Dictionary<string, TopicViewModelCollection<ApplicationBasePageTopicViewModel>> GetCategorizedApplications() {
      var categorizedApplications = new Dictionary<string, TopicViewModelCollection<ApplicationBasePageTopicViewModel>>();
      categorizedApplications.Add(nameof(EnvironmentalSystems), EnvironmentalSystems);
      categorizedApplications.Add(nameof(BusinessSystems), BusinessSystems);
      categorizedApplications.Add(nameof(EngineeredSystems), EngineeredSystems);
      return categorizedApplications;
    }

    /*==========================================================================================================================
    | IS FIRST?
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Returns true if the item is the first item in the list. Automatically toggles to false after the first time it is
    ///   called.
    /// </summary>
    /// <returns></returns>
    public bool IsFirst() {
      if (_isFirst) {
        _isFirst = false;
        return true;
      }
      return false;
    }

  } // Class

} // Namespace