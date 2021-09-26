import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Time } from '../../interfaces/time';
import { RestdataService } from '../../services/restdata.service';

@Component({
  selector: 'app-sena',
  templateUrl: './sena.component.html',
  styleUrls: ['./sena.component.scss']
})
export class SenaComponent implements OnInit {

  timeData: Time = {date: new Date(),number: 0};
  timeDataSet: Time = {date: new Date(),number: 0};
  timeDataList: Time[] = [];

  environmentData: string | undefined;

  constructor(private restdataService: RestdataService, private formBuilder: FormBuilder) { }

  environmentForm = this.formBuilder.group({
    environmentInput: ['AppName']
  });

  timeForm = this.formBuilder.group({
  });

  timeAllForm = this.formBuilder.group({
  });

  ngOnInit() {


  }

  onSubmitEnv() {
    
    this.restdataService.getEnvironmentVariable(this.environmentForm.value.environmentInput).subscribe((data: any)=>{
      console.log('This is the value: ', data);
      this.environmentData = data != null ? data: 'No environment variable' ;
    }) 
    
  }

  onSubmitGetTime() {

    this.restdataService.getRandomTime().subscribe((data: Time)=>{
      console.log(data);
      this.timeData = data;
    })  
    
  }

  onSubmitSetTime() {
    this.restdataService.setTime({date: new Date(),number: this.getRandomInt()}).subscribe((data: Time)=>{
      console.log(data);
      this.timeDataSet = data;
    })  
    
  }

  onSubmitTimeAll() {

    this.restdataService.getTime().subscribe((data: Time[])=>{
      console.log(data);
      this.timeDataList = data;
    })  
    
  }

  // get random int from 0 -1000
  getRandomInt() {
    return Math.floor(Math.random() * 1000);
  }

}
