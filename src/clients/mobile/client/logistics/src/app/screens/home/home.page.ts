import {
	Component,
	ViewChild,
	ElementRef,
	OnInit,
	AfterViewInit,
	OnDestroy,
	Renderer2,
} from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
	selector: 'app-home',
	templateUrl: 'home.page.html',
	styleUrls: ['home.page.scss'],
})
export class HomePage implements OnInit, AfterViewInit, OnDestroy {
	@ViewChild('map') mapElementRef: ElementRef;
	lat: any;
	lng: any;
	title: string = 'Pick Location';
	disableClick: boolean = false;
	mapClickHandler: any;
	googleMaps: any;

	slideOpts = {
		initialSlide: 1,
		speed: 400,
	};

	constructor(private renderer: Renderer2) {}

	ngOnInit() {}

	ngAfterViewInit() {
		window.navigator.geolocation.getCurrentPosition(
			(pos) => {
				this.lat = pos.coords.latitude;
        this.lng = pos.coords.longitude;
        this.loadMap();
			},
			(err) => {
				console.log('geo-loc blocked.');
			}
		);
	}

	ngOnDestroy() {
		this.googleMaps.event.removeListener(this.mapClickHandler);
	}

	private loadMap() {
		this.getGoogleMaps()
			.then((googleMaps) => {
				this.googleMaps = googleMaps;
				const mapEl = this.mapElementRef.nativeElement;
				const map = new googleMaps.Map(mapEl, {
					center: { lat: this.lat, lng: this.lng },
					zoom: 16,
				});

				googleMaps.event.addListenerOnce(map, 'idle', () => {
					this.renderer.addClass(mapEl, 'visible');
				});

				if (!this.disableClick) {
					this.mapClickHandler = map.addListener('click', (event) => {
						const coords = {
							lat: event.latLng.lat(),
							lng: event.latLng.lng(),
						};
						// TODO: Handle Click event
					});
				} else {
					const marker = new googleMaps.Marker({
						position: { lat: this.lat, lng: this.lng },
						color: 'blue',
						map: map,
						title: this.title,
					});
					marker.setMap(map);
				}
			})
			.catch((err) => {
				console.error('MAPS:', err);
			});
	}

	private getGoogleMaps(): Promise<any> {
		const win = window as any;
		const googleModule = win.google;
		if (googleModule && googleModule.maps) {
			return Promise.resolve(googleModule.maps);
		}

		return new Promise((resolve, reject) => {
			const script = document.createElement('script');
			script.src = `https://maps.googleapis.com/maps/api/js?key=${environment.googleApiKey}`;
			script.async = true;
			script.defer = true;
			document.body.appendChild(script);
			script.onload = () => {
				const loadedGoogleModule = win.google;
				if (loadedGoogleModule && loadedGoogleModule.maps) {
					resolve(loadedGoogleModule.maps);
				} else {
					reject('Google maps SDK is not available.');
				}
			};
		});
	}
}
