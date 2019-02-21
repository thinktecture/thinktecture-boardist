import {Directive, ElementRef, Renderer2} from '@angular/core';
import {ControlValueAccessor, NG_VALUE_ACCESSOR} from '@angular/forms';

@Directive({
  // tslint:disable-next-line:directive-selector max-line-length
  selector: 'input[type=file][formControlName]:not([multiple]),input[type=file][formControl]:not([multiple]),input[type=file][ngModel]:not([multiple])',
  // tslint:disable-next-line:use-host-property-decorator
  host: {
    '(input)': 'onChange($event.target.files)',
    '(blur)': 'onTouched()',
  },
  providers: [
    { provide: NG_VALUE_ACCESSOR, useExisting: FileValueAccessorDirective, multi: true },
  ],
})
export class FileValueAccessorDirective implements ControlValueAccessor {
  protected onChange = (_: any) => undefined;
  private onTouched = () => undefined;

  constructor(private readonly renderer: Renderer2, private readonly elementRef: ElementRef<HTMLInputElement>) {
  }

  writeValue(obj: any): void {
    // this is not possible
  }

  registerOnChange(fn: any): void {
    this.onChange = value => fn(value[0]);
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState(isDisabled: boolean): void {
    this.renderer.setProperty(this.elementRef.nativeElement, 'disabled', isDisabled);
  }
}

@Directive({
  // tslint:disable-next-line:directive-selector
  selector: 'input[type=file][formControlName][multiple],input[type=file][formControl][multiple],input[type=file][ngModel][multiple]',
  // tslint:disable-next-line:use-host-property-decorator
  host: {
    '(input)': 'onChange($event.target.files)',
    '(blur)': 'onTouched()',
  },
  providers: [
    { provide: NG_VALUE_ACCESSOR, useExisting: MultipleFileValueAccessorDirective, multi: true },
  ],
})
export class MultipleFileValueAccessorDirective extends FileValueAccessorDirective {
  constructor(renderer: Renderer2, elementRef: ElementRef<HTMLInputElement>) {
    super(renderer, elementRef);
  }

  registerOnChange(fn: any): void {
    this.onChange = value => fn(Array.from(value));
  }
}
