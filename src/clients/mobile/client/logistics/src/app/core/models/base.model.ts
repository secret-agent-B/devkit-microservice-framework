export class BaseModel {
	isNullOrWhiteSpace(param: string) {
		if (param === '' || param === null || param === undefined) {
			return true;
		} else {
			return false;
		}
	}
}