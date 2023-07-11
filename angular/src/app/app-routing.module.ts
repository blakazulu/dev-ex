import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {EmployeesComponent} from './components/employees/employees.component';
import {NotFoundComponent} from './components/not-found/not-found.component';
import {EmployeeDetailsComponent} from './components/employees/employee-details/employee-details.component';
import {ManagersComponent} from './components/managers/managers.component';
import {ManagerDetailsComponent} from './components/managers/manager-details/manager-details.component';

const routes: Routes = [
  {path: '', redirectTo: '/worker', pathMatch: 'full'},
  {path: 'worker', component: EmployeesComponent},
  {path: 'worker/:id', component: EmployeeDetailsComponent},
  // {path: 'tasks', component: TasksComponent},
  {path: 'report', component: ManagersComponent},
  {path: 'report/:id', component: ManagerDetailsComponent},
  {path: '**', component: NotFoundComponent} // Handle 404 Not Found
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}


// ReportController:
// GET /api/report: Retrieve all reports
// POST /api/report: Create a new report
// GET /api/report/{id}: Retrieve a report by ID
// GET /api/report/worker/{workerId}: Retrieve all reports for a worker
// GET /api/report/manager/{managerId}: Retrieve all reports for a manager

// TasksController:
// GET /api/tasks: Retrieve all tasks
// GET /api/tasks/worker/{id}: Retrieve all tasks for worker
// GET /api/tasks/{id}: Retrieve a task by ID
// POST /api/tasks: Create a new task

// WorkerController:
// GET /api/worker: Retrieve all workers
// GET /api/worker/{id}: Retrieve a worker by ID
// GET /api/worker/employees: Retrieve all employees
// GET /api/worker/employees/{id}: Retrieve an employee by ID
// GET /api/worker/managers: Retrieve all managers
// GET /api/worker/managers/{id}: Retrieve a manager by ID
// GET /api/worker/managers/{id}/workers: Retrieve all workers under a manager
