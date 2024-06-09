import { Component } from '@angular/core';
import { CreateRequestHandlerRequest } from '../interfaces/create-request-handler-request';
import { RequestHandlerService } from '../services/request-handler.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-request-handler',
  templateUrl: './create-request-handler.component.html',
  styleUrl: './create-request-handler.component.css'
})
export class CreateRequestHandlerComponent{

  requestHandlerToCreate: CreateRequestHandlerRequest = {} as CreateRequestHandlerRequest;

  constructor(private requestHandlerService: RequestHandlerService, private router: Router) { }

  createRequestHandler(): void { 
    this.requestHandlerService.createRequestHandler(this.requestHandlerToCreate).subscribe({
      next: (response) => {
        alert("RequestHandler created: " + response);
      },
      error: (error) => {
        alert("Error creating RequestHandler: " + error);
      }
    });
  }

  goToHome(): void {
    this.router.navigateByUrl('/home');
  }


}
