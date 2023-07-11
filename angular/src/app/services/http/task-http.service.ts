import {Injectable} from '@angular/core';
import {BaseHttpService} from './base-http.service';
import {Task} from '../../models/task.model';
import {environment} from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TaskHttpService extends BaseHttpService {
  protected baseUrl = `${environment.apiUrl}/tasks/`;

  getAllTasks(): Promise<Task[]> {
    return this.sendRequest<Task[]>('GET', this.baseUrl);
  }

  GetAllTasksForWorker(id:number): Promise<Task[]> {
    return this.sendRequest<Task[]>('GET', `${this.baseUrl}worker/${id}`);
  }

  getTaskById(id: number): Promise<Task> {
    return this.sendRequest<Task>('GET', `${this.baseUrl}${id}`);
  }

  createTask(task: Task): Promise<Task> {
    return this.sendRequest<Task>('POST', this.baseUrl, task);
  }
}
