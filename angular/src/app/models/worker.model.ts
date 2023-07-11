export interface Worker {
  id: number;
  firstName: string;
  lastName: string;
  position: string;
  managerId: number;
  employeesIds: number[];
  reportsIds: number[];
}
