/*! Calendar.js v2.12.2 - Swedish | (c) Bunoon 2024 | MIT License */
var __TRANSLATION_OPTIONS = {
    "dayHeaderNames": [
        "mån",
        "tis",
        "ons",
        "tors",
        "fre",
        "lö",
        "Sol"
    ],
    "dayNames": [
        "måndag",
        "tisdag",
        "onsdag",
        "torsdag",
        "fredag",
        "lördag",
        "söndag"
    ],
    "dayNamesAbbreviated": [
        "mån",
        "tis",
        "ons",
        "tors",
        "fre",
        "lö",
        "Sol"
    ],
    "monthNames": [
        "januari",
        "februari",
        "Mars",
        "april",
        "Maj",
        "juni",
        "juli",
        "augusti",
        "september",
        "oktober",
        "november",
        "december"
    ],
    "monthNamesAbbreviated": [
        "Jan",
        "feb",
        "Mar",
        "apr",
        "Maj",
        "jun",
        "jul",
        "aug",
        "sep",
        "okt",
        "nov",
        "dec"
    ],
    "previousMonthTooltipText": "Förra månaden",
    "nextMonthTooltipText": "Nästa månad",
    "previousDayTooltipText": "Föregående dag",
    "nextDayTooltipText": "Nästa dag",
    "previousWeekTooltipText": "Förra veckan",
    "nextWeekTooltipText": "Nästa vecka",
    "addEventTooltipText": "Lägg till händelse",
    "closeTooltipText": "Stänga",
    "exportEventsTooltipText": "Exportera händelser",
    "todayTooltipText": "I dag",
    "refreshTooltipText": "Uppdatera",
    "searchTooltipText": "Sök",
    "expandDayTooltipText": "Expandera dag",
    "viewAllEventsTooltipText": "Visa alla evenemang",
    "viewFullWeekTooltipText": "Se hela veckan",
    "fromText": "Från:",
    "toText": "Till:",
    "isAllDayText": "Är hela dagen",
    "titleText": "Titel:",
    "descriptionText": "Beskrivning:",
    "locationText": "Plats:",
    "addText": "Lägg till",
    "updateText": "Uppdatering",
    "cancelText": "Annullera",
    "removeEventText": "Avlägsna",
    "addEventTitle": "Lägg till händelse",
    "editEventTitle": "Redigera händelse",
    "exportStartFilename": "exporterade_händelser_",
    "fromTimeErrorMessage": "Vänligen välj en giltig \"Från\"-tid.",
    "toTimeErrorMessage": "Vänligen välj en giltig \"Till\"-tid.",
    "toSmallerThanFromErrorMessage": "Välj ett \"Till\"-datum som är större än \"Från\"-datumet.",
    "titleErrorMessage": "Ange ett värde i fältet \"Titel\" (inget tomt utrymme).",
    "stText": "",
    "ndText": "",
    "rdText": "",
    "thText": "",
    "yesText": "Ja",
    "noText": "Nej",
    "allDayText": "Hela dagen",
    "allEventsText": "Alla evenemang",
    "toTimeText": "till",
    "confirmEventRemoveTitle": "Bekräfta borttagning av händelse",
    "confirmEventRemoveMessage": "Att ta bort denna händelse kan inte ångras. ",
    "okText": "OK",
    "exportEventsTitle": "Exportera händelser",
    "selectColorsText": "Välj Färger",
    "backgroundColorText": "Bakgrundsfärg:",
    "textColorText": "Text färg:",
    "borderColorText": "Gräns ​​färg:",
    "searchEventsTitle": "Sök händelser",
    "previousText": "Tidigare",
    "nextText": "Nästa",
    "matchCaseText": "Liknande fall",
    "repeatsText": "Upprepar:",
    "repeatDaysToExcludeText": "Upprepa dagar att utesluta:",
    "daysToExcludeText": "Dagar att utesluta:",
    "seriesIgnoreDatesText": "Serien ignorera datum:",
    "repeatsNever": "Aldrig",
    "repeatsEveryDayText": "Varje dag",
    "repeatsEveryWeekText": "Varje vecka",
    "repeatsEvery2WeeksText": "Varannan vecka",
    "repeatsEveryMonthText": "Varje månad",
    "repeatsEveryYearText": "Varje år",
    "repeatsCustomText": "Beställnings:",
    "repeatOptionsTitle": "Upprepa alternativ",
    "moreText": "Mer",
    "includeText": "Omfatta:",
    "minimizedTooltipText": "Minimera",
    "restoreTooltipText": "Återställ",
    "removeAllEventsInSeriesText": "Ta bort alla händelser i serien",
    "createdText": "Skapad:",
    "organizerNameText": "Arrangör:",
    "organizerEmailAddressText": "Arrangörens e-post:",
    "enableFullScreenTooltipText": "Slå på helskärmsläge",
    "disableFullScreenTooltipText": "Stäng av helskärmsläge",
    "idText": "ID:",
    "expandMonthTooltipText": "Expandera månad",
    "repeatEndsText": "Upprepa slutar:",
    "noEventsAvailableText": "Inga evenemang tillgängliga.",
    "viewFullWeekText": "Se hela veckan",
    "noEventsAvailableFullText": "Det finns inga tillgängliga evenemang att se.",
    "clickText": "Klick",
    "hereText": "här",
    "toAddANewEventText": "för att lägga till en ny händelse.",
    "weekText": "Vecka",
    "groupText": "Grupp:",
    "configurationTooltipText": "Konfiguration",
    "configurationTitleText": "Konfiguration",
    "groupsText": "Grupper",
    "eventNotificationTitle": "Calendar.js",
    "eventNotificationBody": "Händelsen '{0}' har startat.",
    "optionsText": "Alternativ:",
    "startsWithText": "Börjar med",
    "endsWithText": "Slutar med",
    "containsText": "Innehåller",
    "displayTabText": "Visa",
    "enableAutoRefreshForEventsText": "Aktivera automatisk uppdatering för händelser",
    "enableBrowserNotificationsText": "Aktivera webbläsaraviseringar",
    "enableTooltipsText": "Aktivera verktygstips",
    "dayText": "dag",
    "daysText": "dagar",
    "hourText": "timme",
    "hoursText": "timmar",
    "minuteText": "minut",
    "minutesText": "minuter",
    "enableDragAndDropForEventText": "Aktivera dra",
    "organizerTabText": "Arrangör",
    "removeEventsTooltipText": "Ta bort händelser",
    "confirmEventsRemoveTitle": "Bekräfta borttagning av händelser",
    "confirmEventsRemoveMessage": "Att ta bort dessa icke-repeterande händelser kan inte ångras. ",
    "eventText": "Händelse",
    "optionalText": "Frivillig",
    "urlText": "URL:",
    "openUrlText": "Öppna URL",
    "thisWeekTooltipText": "Denna vecka",
    "dailyText": "Dagligen",
    "weeklyText": "Varje vecka",
    "monthlyText": "En gång i månaden",
    "yearlyText": "Årlig",
    "repeatsByCustomSettingsText": "Genom anpassade inställningar",
    "lastUpdatedText": "Senast uppdaterad:",
    "advancedText": "Avancerad",
    "copyText": "Kopiera",
    "pasteText": "Klistra",
    "duplicateText": "Duplicera",
    "showAlertsText": "Visa varningar",
    "selectDatePlaceholderText": "Välj datum...",
    "hideDayText": "Göm dag",
    "notSearchText": "Inte (motsatsen)",
    "showHolidaysInTheDisplaysText": "Visa helgdagar i huvuddisplayen och titelfälten",
    "newEventDefaultTitle": "* Nytt event",
    "urlErrorMessage": "Vänligen ange en giltig URL i fältet \"Url\" (eller lämna tomt).",
    "searchTextBoxPlaceholder": "Sök rubrik, beskrivning osv...",
    "currentMonthTooltipText": "Denna månad",
    "cutText": "Skära",
    "showMenuTooltipText": "Visa meny",
    "eventTypesText": "Händelsetyper",
    "lockedText": "Låst:",
    "typeText": "Typ:",
    "sideMenuHeaderText": "Calendar.js",
    "sideMenuDaysText": "dagar",
    "visibleDaysText": "Synliga dagar",
    "previousYearTooltipText": "Förra året",
    "nextYearTooltipText": "Nästa år",
    "showOnlyWorkingDaysText": "Visa endast arbetsdagar",
    "exportFilenamePlaceholderText": "Namn (valfritt)",
    "errorText": "Fel",
    "exportText": "Exportera",
    "configurationUpdatedText": "Konfigurationen uppdaterad.",
    "eventAddedText": "{0} händelse har lagts till.",
    "eventUpdatedText": "{0} händelse uppdaterad.",
    "eventRemovedText": "{0} händelse har tagits bort.",
    "eventsRemovedText": "{0} händelser har tagits bort.",
    "eventsExportedToText": "Händelser exporterade till {0}.",
    "eventsPastedText": "{0} händelser klistrade in.",
    "eventsExportedText": "Händelser exporterade.",
    "copyToClipboardOnlyText": "Kopiera endast till urklipp",
    "workingDaysText": "Arbetsdagar",
    "weekendDaysText": "Helgdagar",
    "showAsBusyText": "Visa som upptagen",
    "selectAllText": "Välj alla",
    "selectNoneText": "Välj ingen",
    "importEventsTooltipText": "Importera händelser",
    "eventsImportedText": "{0} händelser importerade.",
    "viewFullYearTooltipText": "Se hela året",
    "currentYearTooltipText": "Nuvarande år",
    "alertOffsetText": "Varningsförskjutning (minuter):",
    "viewFullDayTooltipText": "Se hela dagen",
    "confirmEventUpdateTitle": "Bekräfta händelseuppdatering",
    "confirmEventUpdateMessage": "Vill du uppdatera eventet från och med nu, eller hela serien?",
    "forwardText": "Fram",
    "seriesText": "Serier",
    "viewTimelineTooltipText": "Se tidslinje",
    "nextPropertyTooltipText": "Nästa fastighet",
    "noneText": "(ingen)",
    "shareText": "Dela med sig",
    "shareStartFilename": "shared_events_",
    "previousPropertyTooltipText": "Tidigare fastighet",
    "jumpToDateTitle": "Hoppa till datum",
    "goText": "Gå"
};