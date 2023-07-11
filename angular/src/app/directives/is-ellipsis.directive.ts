import {Directive, ElementRef, HostListener} from '@angular/core';

@Directive({
  selector: '[isEllipsis]'
})
export class IsEllipsisDirective {

  constructor(private elementRef: ElementRef) {
  }

  @HostListener('mouseenter')
  onMouseEnter(): void {
    setTimeout(() => {
      const element = this.elementRef.nativeElement;
      if (element.offsetWidth < element.scrollWidth) {
        element.title = element.textContent;
      } else if (element.title) element.removeAttribute('title');
    }, 500);
  }
}
