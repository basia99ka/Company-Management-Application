namespace Client.ApplicationStates
{
    public class State
    {
        //Department

        public Action? Action { get; set; }
        public bool ShowDepartment { get; set; }
        public void DepartmentClicked()
        {
            ResetAllDepartments();
            ShowDepartment = true;
            Action?.Invoke();

        }
        // Team
        public bool ShowBranch { get; set; }
        public void BranchClicked()
        {
            ResetAllDepartments();
            ShowBranch = true;
            Action?.Invoke();

        }
        // Project
        public bool ShowProject { get; set; }
        public void ProjectClicked()
        {
            ResetAllDepartments();
            ShowProject = true;
            Action?.Invoke();

        }

        // User
        public bool ShowUser { get; set; }
        public void UserClicked()
        {
            ResetAllDepartments();
            ShowUser = true;
            Action?.Invoke();

        }

        // Employee
        public bool ShowEmployee { get; set; }
        public void EmployeeClicked()
        {
            ResetAllDepartments();
            ShowEmployee = true;
            Action?.Invoke();

        }
        private void ResetAllDepartments()
        {
            ShowDepartment = false;
            ShowBranch = false;
            ShowProject = false;
            ShowUser = false;
            ShowEmployee = false;
        }


    }
}