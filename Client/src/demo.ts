let data:number | string;

data='42';

data=10;

interface ICar{
   color:string;
   model:string;
   topSpeed?:number;
}

const car1: ICar ={
   color:'blue',
   model:'bmw'
};
const car2 :ICar ={
  color:'red',
  model:'mercedes',
  topSpeed:100
};

const multipy=(x:any,y:any):string=>{
  return (x * y).toString();
}

