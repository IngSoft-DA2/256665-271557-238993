import { Component } from '@angular/core';
import { CreateRequestHandlerRequest } from '../interfaces/create-request-handler-request';
import { RequestHandlerService } from '../services/request-handler.service';

@Component({
  selector: 'app-create-request-handler',
  templateUrl: './create-request-handler.component.html',
  styleUrl: './create-request-handler.component.css'
})
export class CreateRequestHandlerComponent{

  requestHandlerToCreate: CreateRequestHandlerRequest = {} as CreateRequestHandlerRequest;

  constructor(private requestHandlerService: RequestHandlerService) { }

  createRequestHandler(): void { 
    alert("Creating RequestHandler: " + this.requestHandlerToCreate.firstname);
    this.requestHandlerService.createRequestHandler(this.requestHandlerToCreate).subscribe({
      next: (response) => {
        console.log("RequestHandler created: " + response);
      },
      error: (error) => {
        console.log("Error creating RequestHandler: " + error);
      }
    });
  }


}
