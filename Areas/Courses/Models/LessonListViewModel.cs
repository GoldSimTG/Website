﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Goldsim
| Project       Website
\=============================================================================================================================*/
using System.Collections.ObjectModel;
using GoldSim.Web.Models;
using OnTopic.AspNetCore.Mvc.Models;

namespace GoldSim.Web.Courses.Models {

  /*============================================================================================================================
  | VIEW MODEL: LESSON LIST
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about all sibling <c>Lesson</c>
  ///   topics.
  /// </summary>
  public class LessonListViewModel: NavigationViewModel<TrackedNavigationTopicViewModel> {

    /*==========================================================================================================================
    | TRACKING EVENTS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a list of events that should be tracked by Google Analytics.
    /// </summary>
    public Collection<TrackingEventViewModel> TrackingEvents { get; } = new();

  } // Class
} // Namespace