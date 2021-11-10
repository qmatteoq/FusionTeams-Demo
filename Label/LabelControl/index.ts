import {IInputs, IOutputs} from "./generated/ManifestTypes";

export class LabelControl implements ComponentFramework.StandardControl<IInputs, IOutputs> {

	private labelElement: HTMLLabelElement;
	private _container: HTMLDivElement;
	private _context: ComponentFramework.Context<IInputs>;
	private listElement: HTMLUListElement;

	/**
	 * Empty constructor.
	 */
	constructor()
	{

	}

	/**
	 * Used to initialize the control instance. Controls can kick off remote server calls and other initialization actions here.
	 * Data-set values are not initialized here, use updateView.
	 * @param context The entire property bag available to control via Context Object; It contains values as set up by the customizer mapped to property names defined in the manifest, as well as utility functions.
	 * @param notifyOutputChanged A callback method to alert the framework that the control has new outputs ready to be retrieved asynchronously.
	 * @param state A piece of data that persists in one session for a single user. Can be set at any point in a controls life cycle by calling 'setControlState' in the Mode interface.
	 * @param container If a control is marked control-type='standard', it will receive an empty div element within which it can render its content.
	 */
	public init(context: ComponentFramework.Context<IInputs>, notifyOutputChanged: () => void, state: ComponentFramework.Dictionary, container:HTMLDivElement): void
	{
		this._context = context;
		this._container = container;

		this.labelElement = document.createElement("label");
		this.labelElement.textContent = context.parameters.header.raw;

		this._container.appendChild(this.labelElement);

		this.listElement = document.createElement("ul");
		this.listElement.setAttribute("id", "myList");
		this.getData();

		this._container.appendChild(this.listElement);
	}

	public async getData(count: number = 5): Promise<void> {
		var json = await fetch("https://fusion-customers-api.azurewebsites.net/customers");
		var jsonData = await json.json();
		
		for (var i = 0; i < count; i++) {
			var item = document.createElement("li");
			item.textContent = jsonData[i].name + " " + jsonData[i].surname;
			this.listElement.appendChild(item);
		}
	}

	/**
	 * Called when any value in the property bag has changed. This includes field values, data-sets, global values such as container height and width, offline status, control metadata values such as label, visible, etc.
	 * @param context The entire property bag available to control via Context Object; It contains values as set up by the customizer mapped to names defined in the manifest, as well as utility functions
	 */
	public async updateView(context: ComponentFramework.Context<IInputs>): Promise<void>
	{
		this.labelElement.textContent = context.parameters.header.raw;
	}

	/**
	 * It is called by the framework prior to a control receiving new data.
	 * @returns an object based on nomenclature defined in manifest, expecting object[s] for property marked as “bound” or “output”
	 */
	public getOutputs(): IOutputs
	{
		return {
			
		};
	}

	/**
	 * Called when the control is to be removed from the DOM tree. Controls should use this call for cleanup.
	 * i.e. cancelling any pending remote calls, removing listeners, etc.
	 */
	public destroy(): void
	{
		// Add code to cleanup control if necessary
	}
}
