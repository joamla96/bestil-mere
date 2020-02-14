import {MealItem} from './mealItem';
import {ExtraMealItem} from './extraMealItem';

export class Meal {
	name: string;
	mealItems: MealItem[];
	extraMealItems: ExtraMealItem[];
}
