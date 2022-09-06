import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { BussyService } from "../services/bussy.service"
import { delay, finalize, Observable } from "rxjs";
import { Injectable } from "@angular/core";

@Injectable()
export class LoadingInterceptor  implements HttpInterceptor{


  constructor(private busyService:BussyService){

  }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this.busyService.bussy();
    return next.handle(req).pipe(
      delay(1000),
      finalize(()=>{
        this.busyService.idle();
      })
    );
  }

}
