export interface Reward {
  code: Code;
  distance: number;
  tripId: number;
}
export enum Code {
  VEGAN = 1,
  TRANSPORT = 2,
  SHOWER = 3,
  BAG = 4,
  RECYLCING = 5,
  TURNOFF = 6,
  DEGREE = 7,
}
