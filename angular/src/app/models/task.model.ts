export interface Task {
  id: number;
  text: string;
  assigneeId: number;
  assignedDate: Date;
  dueDate: Date;
}
