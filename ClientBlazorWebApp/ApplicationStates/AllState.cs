﻿namespace ClientBlazorWebApp.ApplicationStates
{
    public class AllState
    {
        public Action? Action { get; set; }
        
        //General Department
        public bool ShowGeneralDepartment { get; set; }
        public void GeneralDepartmentClicked()
        {
            ResetAllDepartments();
            ShowGeneralDepartment = true;
            Action?.Invoke();
        }

        //Department
        public bool ShowDepartment { get; set; }
        public void DepartmentClicked()
        {
            ResetAllDepartments();
            ShowDepartment = true;
            Action?.Invoke();
        }

        //Branch
        public bool ShowBranch { get; set; }
        public void BranchClicked()
        {
            ResetAllDepartments();
            ShowBranch = true;
            Action?.Invoke();
        }

        //Country
        public bool ShowCountry { get; set; }
        public void CountryClicked()
        {
            ResetAllDepartments();
            ShowCountry = true;
            Action?.Invoke();
        }
        //City
        public bool ShowCity { get; set; }
        public void CityClicked()
        {
            ResetAllDepartments();
            ShowCity = true;
            Action?.Invoke();
        }
        //Town
        public bool ShowTown { get; set; }
        public void TownClicked()
        {
            ResetAllDepartments();
            ShowTown = true;
            Action?.Invoke();
        }

        //User
        public bool ShowUser { get; set; }
        public void UserClicked()
        {
            ResetAllDepartments();
            ShowUser = true;
            Action?.Invoke();
        }

        //Employee
        public bool ShowEmployee { get; set; } = true;
        public void EmployeeClicked()
        {
            ResetAllDepartments();
            ShowEmployee = true;
            Action?.Invoke();
        }

        //health
        public bool ShowHealth { get; set; }
        public void DoctorClicked()
        {
            ResetAllDepartments();
            ShowHealth = true;
            Action?.Invoke();
        }
        //sanction
        public bool ShowSanction { get; set; }
        public void SanctionClicked()
        {
            ResetAllDepartments();
            ShowSanction = true;
            Action?.Invoke();
        }
        //sanction Type
        public bool ShowSanctionType { get; set; }
        public void SanctionTypeClicked()
        {
            ResetAllDepartments();
            ShowSanctionType = true;
            Action?.Invoke();
        }
        //overtime
        public bool ShowOvertime { get; set; }
        public void OvertimeClicked()
        {
            ResetAllDepartments();
            ShowOvertime = true;
            Action?.Invoke();
        }
        //overtime Type
        public bool ShowOvertimeType { get; set; }
        public void OvertimeTypeClicked()
        {
            ResetAllDepartments();
            ShowOvertimeType = true;
            Action?.Invoke();
        }
        //vacation
        public bool ShowVacation { get; set; }
        public void VacationClicked()
        {
            ResetAllDepartments();
            ShowVacation = true;
            Action?.Invoke();
        }
        //vacation Type
        public bool ShowVacationType { get; set; }
        public void VacationTypeClicked()
        {
            ResetAllDepartments();
            ShowVacationType = true;
            Action?.Invoke();
        }
        //Alert
        public bool? ShowAlert { get; set; }
        public string message { get; set; } = string.Empty;
        public void TriggerAlert(bool? alertStatus,string _message)
        {
            message = _message;
            ShowAlert = alertStatus; 
            Action?.Invoke();
            
        }

        private void ResetAllDepartments()
        {
            ShowGeneralDepartment = false;
            ShowDepartment = false;
            ShowBranch = false;
            ShowCountry = false;
            ShowCity = false;
            ShowTown = false;
            ShowUser = false;

            ShowEmployee = false;
            ShowHealth = false;

            ShowVacation = false;
            ShowSanction = false;
            ShowOvertime = false;


            ShowVacationType = false;
            ShowSanctionType = false;
            ShowOvertimeType = false;
        }
    }
}
