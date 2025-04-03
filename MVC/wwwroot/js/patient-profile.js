"use strict";
let patientAccordionButton = document.getElementById("patient-more-information-button");
let patientArrowIcon = document.getElementById("patient-arrow-icon");
let patientTabs = document.querySelectorAll(".patient-tab");
if (patientAccordionButton != null)
    patientAccordionButton.onclick = () => patientArrowIcon === null || patientArrowIcon === void 0 ? void 0 : patientArrowIcon.classList.toggle("up");
patientTabs === null || patientTabs === void 0 ? void 0 : patientTabs.forEach(tab => {
    tab === null || tab === void 0 ? void 0 : tab.addEventListener("click", function () {
        var _a, _b;
        patientTabs.forEach(x => {
            var _a, _b;
            x.classList.remove("patient-tab-active");
            let selectedTab = document.getElementById((_b = (_a = x.dataset) === null || _a === void 0 ? void 0 : _a.tab) !== null && _b !== void 0 ? _b : "none");
            if (selectedTab != null) {
                selectedTab.classList.remove("d-flex");
                selectedTab.classList.add("d-none");
            }
        });
        this.classList.add("patient-tab-active");
        let selectedTab = document.getElementById((_b = (_a = this.dataset) === null || _a === void 0 ? void 0 : _a.tab) !== null && _b !== void 0 ? _b : "none");
        if (selectedTab != null) {
            selectedTab.classList.remove("d-none");
            selectedTab.classList.add("d-flex");
        }
    });
});
