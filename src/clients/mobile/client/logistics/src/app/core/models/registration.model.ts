import { BaseModel } from './base.model';
import { GovInfo } from './gov.info.model';
import { VehicleInfo } from './vehicle.info.model';

export class MemberRegistration extends BaseModel {
	// User information
	public firstName: string;
	public middleName: string;
	public lastName: string;

	// Contact Information
	public emailAddress: string;
	public confirmEmailAddress: string;
	public phoneNumber: string;

	// Address Information
	public address1: string;
	public address2: string;
	public city: string;
	public province: string;
	public country: string;
	public zipCode: string;

	// MISC Information
	public photo: string;

	public TryNullValidation(): MemberRegistration {
		if (
			this.isNullOrWhiteSpace(this.firstName) ||
			this.isNullOrWhiteSpace(this.middleName) ||
			this.isNullOrWhiteSpace(this.lastName) ||
			this.isNullOrWhiteSpace(this.emailAddress) ||
			this.isNullOrWhiteSpace(this.confirmEmailAddress) ||
			this.isNullOrWhiteSpace(this.phoneNumber) ||
			this.isNullOrWhiteSpace(this.address1) || 
			this.isNullOrWhiteSpace(this.city) ||
			this.isNullOrWhiteSpace(this.province) ||
			this.isNullOrWhiteSpace(this.country) ||
			this.isNullOrWhiteSpace(this.zipCode)||
			this.isNullOrWhiteSpace(this.photo)
		) {
			return null;
		}

		return this;
	}
}

export class DriverRegistration extends MemberRegistration {
	constructor() {
        super();
		this.govt = new GovInfo();
		this.vehicle = new VehicleInfo();
	}

	// Gov Information
	public govt: GovInfo;

	// Vehicle Information
	public vehicle: VehicleInfo;

	public TryNullValidation(): DriverRegistration {
		const member = super.TryNullValidation();

		if (member === null) {
			return null;
		}

		if (
			this.isNullOrWhiteSpace(this.govt.number) ||
			this.isNullOrWhiteSpace(this.govt.type) ||
			this.isNullOrWhiteSpace(this.govt.photo) ||
			this.isNullOrWhiteSpace(this.vehicle.year) ||
			this.isNullOrWhiteSpace(this.vehicle.make) ||
			this.isNullOrWhiteSpace(this.vehicle.model) ||
			this.isNullOrWhiteSpace(this.vehicle.plateNumber) ||
			this.isNullOrWhiteSpace(this.vehicle.vin)
		) {
			return null;
		}

		return this;
	}
}
